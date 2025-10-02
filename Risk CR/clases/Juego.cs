using Risk_CR.Jugadores;
using System;
using System.Windows.Forms;

namespace Risk_CR
{
    public class Juego
    {
        private static Juego _instance;
        private int turnoActual;
        private int indiceInicioColocacion;
        public static Juego Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Juego();
                return _instance;
            }
        }

        public ListaGod<Jugador> Jugadores { get; set; }
        public ListaGod<Territorio> Territorios { get; set; }
        public Jugador JugadorActual { get; set; }
        public Jugador EjercitoNeutral { get; set; }
        public FaseTurno FaseActual { get; set; }
        public int ContadorFibonacci { get; set; } = 2;
        public bool JuegoTerminado { get; set; } = false;
        public AtaqueData AtaqueActual { get; private set; }
        public enum FaseTurno { ColocacionInicial, Refuerzo, Ataque, Planeacion }

        private Juego()
        {
            Jugadores = new ListaGod<Jugador>();
            Territorios = new ListaGod<Territorio>();
            FaseActual = FaseTurno.ColocacionInicial;
            turnoActual = 0;
        }

        public void IniciarJuego(ListaGod<Jugador> jugadores, ListaGod<Territorio> territorios)
        {
            Jugadores = jugadores;
            Territorios = territorios;

            for (int i = 0; i < Jugadores.Count; i++)
            {
                if (Jugadores.Obtener(i).Nombre == "Ejercito Neutral")
                {
                    EjercitoNeutral = Jugadores.Obtener(i);
                    break;
                }
            }

            if (EjercitoNeutral != null)
            {
                Jugadores.Remover(EjercitoNeutral);
                Jugadores.Agregar(EjercitoNeutral);
            }

            DistribuirTerritorios();

            for (int i = 0; i < Jugadores.Count; i++)
            {
                if (Jugadores.Obtener(i) != EjercitoNeutral)
                {
                    turnoActual = i;
                    JugadorActual = Jugadores.Obtener(i);
                    indiceInicioColocacion = turnoActual;
                    break;
                }
            }

            FaseActual = FaseTurno.ColocacionInicial;
        }

        public void AvanzarFase()
        {
            switch (FaseActual)
            {
                case FaseTurno.ColocacionInicial:

                    break;

                case FaseTurno.Planeacion:
                    AvanzarTurno();
                    FaseActual = FaseTurno.Refuerzo;
                    DarRefuerzosAlJugador();
                    break;

                case FaseTurno.Refuerzo:
                    FaseActual = FaseTurno.Ataque;
                    break;

                case FaseTurno.Ataque:
                    FaseActual = FaseTurno.Planeacion;
                    break;
            }
        }

        private bool EsJugadorActivo(Jugador j)
        {
            return j != EjercitoNeutral
                && j.TerritoriosControlados.Count > 0;
        }

        public void AvanzarTurno()
        {
            int siguiente = turnoActual;
            do
            {
                siguiente = (siguiente + 1) % Jugadores.Count;
            }
            while (!EsJugadorActivo(Jugadores.Obtener(siguiente)));

            turnoActual = siguiente;
            JugadorActual = Jugadores.Obtener(turnoActual);
        }


        private void DistribuirTerritorios()
        {
            Territorio[] arrayTerritorios = new Territorio[Territorios.Count];
            for (int i = 0; i < Territorios.Count; i++)
            {
                arrayTerritorios[i] = Territorios.Obtener(i);
            }

            Random rnd = new Random();
            for (int i = 0; i < arrayTerritorios.Length; i++)
            {
                int randomIndex = rnd.Next(arrayTerritorios.Length);
                Territorio temp = arrayTerritorios[i];
                arrayTerritorios[i] = arrayTerritorios[randomIndex];
                arrayTerritorios[randomIndex] = temp;
            }

            for (int i = 0; i < arrayTerritorios.Length; i++)
            {
                if (i < 14)
                {
                    Jugadores.Obtener(0).AgregarTerritorio(arrayTerritorios[i]);
                    arrayTerritorios[i].CambiarOcupante(Jugadores.Obtener(0));
                }
                else if (i < 28)
                {
                    Jugadores.Obtener(1).AgregarTerritorio(arrayTerritorios[i]);
                    arrayTerritorios[i].CambiarOcupante(Jugadores.Obtener(1));
                }
                else
                {
                    EjercitoNeutral.AgregarTerritorio(arrayTerritorios[i]);
                    arrayTerritorios[i].CambiarOcupante(EjercitoNeutral);
                }

                arrayTerritorios[i].AgregarTropas(1);
            }


            for (int j = 0; j < Jugadores.Count; j++)
            {
                if (Jugadores.Obtener(j) == EjercitoNeutral)
                {
                    Jugadores.Obtener(j).TropasDisponibles = 14;
                }
                else
                {
                    Jugadores.Obtener(j).TropasDisponibles = 26;
                }
            }
        }
        public bool ColocarTropaInicial(Territorio territorio)
        {
            if (FaseActual != FaseTurno.ColocacionInicial ||
                !TerritorioPerteneceAJugadorActual(territorio) ||
                JugadorActual.TropasDisponibles <= 0)
            {
                return false;
            }

            territorio.AgregarTropas(1);
            JugadorActual.TropasDisponibles--;


            SiguienteJugadorColocacion();

            return true;
        }

        private void SiguienteJugadorColocacion()
        {
            if (TodosJugadoresTerminaron())
            {
                FaseActual = FaseTurno.Refuerzo;


                turnoActual = indiceInicioColocacion;
                JugadorActual = Jugadores.Obtener(turnoActual);


                DarRefuerzosAlJugador();

                return;
            }

            do
            {
                turnoActual = (turnoActual + 1) % Jugadores.Count;
                JugadorActual = Jugadores.Obtener(turnoActual);
            }
            while (JugadorActual.TropasDisponibles <= 0);


            if (JugadorActual == EjercitoNeutral)
            {
                ColocarTropaNeutralAutomatica();
                SiguienteJugadorColocacion();
            }
        }

        public void DarRefuerzosAlJugador()
        {

            if (FaseActual != FaseTurno.Refuerzo) return;


            int cantidadTerritorios = JugadorActual.TerritoriosControlados.Count;


            int refuerzosBase = cantidadTerritorios / 3;
            if (refuerzosBase < 3) refuerzosBase = 3;


            int bonusExtra = 0;


            if (JugadorTieneTodaLaProvincia("San José")) bonusExtra += 2;


            if (JugadorTieneTodaLaProvincia("Alajuela")) bonusExtra += 2;

            if (JugadorTieneTodaLaProvincia("Cartago")) bonusExtra += 3;


            if (JugadorTieneTodaLaProvincia("Limon")) bonusExtra += 3;

            if (JugadorTieneTodaLaProvincia("Guanacaste")) bonusExtra += 5;


            if (JugadorTieneTodaLaProvincia("Puntarenas")) bonusExtra += 7;


            int totalRefuerzos = refuerzosBase + bonusExtra;

            JugadorActual.TropasDisponibles += totalRefuerzos;
        }


        private bool JugadorTieneTodaLaProvincia(string nombreProvincia)
        {
            int territoriosEnProvincia = 0;
            int territoriosQueTiene = 0;


            for (int i = 0; i < Territorios.Count; i++)
            {
                Territorio t = Territorios.Obtener(i);

                if (t.Provincia == nombreProvincia)
                {
                    territoriosEnProvincia++;


                    if (t.Ocupante == JugadorActual)
                    {
                        territoriosQueTiene++;
                    }
                }
            }

            return territoriosQueTiene == territoriosEnProvincia;
        }

        private void ColocarTropaNeutralAutomatica()
        {
            if (EjercitoNeutral.TropasDisponibles <= 0) return;

            Random rnd = new Random();

            int randomIndex = rnd.Next(EjercitoNeutral.TerritoriosControlados.Count);
            Territorio territorio = EjercitoNeutral.TerritoriosControlados.Obtener(randomIndex);

            territorio.AgregarTropas(1);
            EjercitoNeutral.TropasDisponibles--;
        }

        private bool TodosJugadoresTerminaron()
        {
            for (int i = 0; i < Jugadores.Count; i++)
            {
                Jugador jugador = Jugadores.Obtener(i);
                if (jugador.TropasDisponibles > 0)
                {
                    return false;
                }
            }
            return true;
        }

        public bool ReforzarTerritorio(Territorio territorio, int cantidad)
        {
            if (FaseActual != FaseTurno.Refuerzo ||
                JugadorActual.TropasDisponibles < cantidad ||
                !TerritorioPerteneceAJugadorActual(territorio))
            {
                return false;
            }

            territorio.AgregarTropas(cantidad);
            JugadorActual.TropasDisponibles -= cantidad;
            return true;
        }

        public bool TerritorioPerteneceAJugadorActual(Territorio territorio)
        {
            return territorio.Ocupante == JugadorActual;
        }
        public class AtaqueData
        {
            public Territorio Origen { get; set; }
            public Territorio Destino { get; set; }
            public int TropasAtacante { get; set; }
            public int TropasDefensor { get; set; }
        }

        private bool HayRutaControlada(Territorio origen, Territorio destino)
        {
            var visitados = new ListaGod<Territorio>();
            var cola = new ListaGod<Territorio>();
            int cabeza = 0;

            cola.Agregar(origen);
            visitados.Agregar(origen);

            while (cabeza < cola.Count)
            {
                Territorio actual = cola.Obtener(cabeza++);
                if (actual == destino)
                    return true;

               
                for (int i = 0; i < actual.TerritoriosAdyacentes.Count; i++)
                {
                    var vecino = actual.TerritoriosAdyacentes.Obtener(i);
                    if (vecino.Ocupante == JugadorActual && !visitados.Contiene(vecino))
                    {
                        cola.Agregar(vecino);
                        visitados.Agregar(vecino);
                    }
                }
            }

            return false;
        }



        public bool IniciarAtaque(Territorio origen, Territorio destino)
        {
            if (FaseActual != FaseTurno.Ataque ||
                !TerritorioPerteneceAJugadorActual(origen) ||
                destino.Ocupante == JugadorActual ||
                !origen.EsAdyacente(destino) ||
                !origen.PuedeAtacar())
            {
                return false;
            }

            AtaqueActual = new AtaqueData
            {
                Origen = origen,
                Destino = destino
            };
            return true;
        }

        public Dado ResolverAtaque(int tropasAtacante, int tropasDefensor)
        {
            if (AtaqueActual == null) return null;

            AtaqueActual.TropasAtacante = tropasAtacante;
            AtaqueActual.TropasDefensor = tropasDefensor;

            var dado = new Dado();
            dado.LanzarYComparar(tropasAtacante, tropasDefensor);

         
            AtaqueActual.Origen.RemoverTropas(dado.EjercitosPerdidosAtacante);
            AtaqueActual.Destino.RemoverTropas(dado.EjercitosPerdidosDefensor);

           
            if (AtaqueActual.Destino.Tropas == 0)
            {
                ConquistarTerritorio(AtaqueActual.Destino);
            }

            return dado;
        }

        private void ConquistarTerritorio(Territorio territorioConquistado)
        {
            var antiguoOcupante = territorioConquistado.Ocupante as Jugador;
            if (antiguoOcupante != null)
            {
                antiguoOcupante.RemoverTerritorio(territorioConquistado);
            }

            JugadorActual.AgregarTerritorio(territorioConquistado);
            territorioConquistado.CambiarOcupante(JugadorActual);

           
            int tropasAMover = Math.Min(AtaqueActual.TropasAtacante, AtaqueActual.Origen.Tropas - 1);
            AtaqueActual.Origen.RemoverTropas(tropasAMover);
            territorioConquistado.AgregarTropas(tropasAMover);

        
            if (territorioConquistado.TieneCarta)
            {
                Carta nuevaCarta = territorioConquistado.ReclamarCarta();
                if (nuevaCarta != null)
                {
                    JugadorActual.RecibirCarta(nuevaCarta);
                    MessageBox.Show($"¡Recibiste una carta: {nuevaCarta.Territorio} - {nuevaCarta.Tipo}!", "Carta Obtenida");
                }
            }
            


        }
       
        public bool MoverTropasPlaneacion(Territorio origen, Territorio destino, int cantidad)
        {
            if (FaseActual != FaseTurno.Planeacion)
                return false;

            if (origen.Ocupante != JugadorActual ||
                destino.Ocupante != JugadorActual)
                return false;

            if (origen.Tropas < 2 || cantidad < 1 || cantidad > origen.Tropas - 1)
                return false;

            if (!HayRutaControlada(origen, destino))
                return false;

            origen.RemoverTropas(cantidad);
            destino.AgregarTropas(cantidad);

           
            AvanzarFase();
            return true;
        }

    }

}