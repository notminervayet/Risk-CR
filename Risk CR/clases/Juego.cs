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

        private const int TOTAL_TERRITORIOS = 42;
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
         
            bool modoTresJugadores = false;
            EjercitoNeutral = null;

            for (int i = 0; i < Jugadores.Count; i++)
            {
                if (Jugadores.Obtener(i).Nombre == "Ejercito Neutral")
                {
                    EjercitoNeutral = Jugadores.Obtener(i);
                    break;
                }
            }
            
            modoTresJugadores = (EjercitoNeutral == null);

            for (int i = 0; i < Jugadores.Count; i++)
            {
                Jugador jugador = Jugadores.Obtener(i);
                if (jugador.Nombre.ToLower() == "ejercito neutral" && modoTresJugadores)
                {
                    throw new InvalidOperationException("No se permite usar 'Ejercito Neutral' como nombre de jugador.");
                }
            }

            DistribuirTerritorios();
         
            for (int i = 0; i < Jugadores.Count; i++)
            {
                Jugador jugador = Jugadores.Obtener(i);
                if (modoTresJugadores || jugador != EjercitoNeutral)
                {
                    turnoActual = i;
                    JugadorActual = jugador;
                    indiceInicioColocacion = turnoActual;
                    break;
                }
            }
            FaseActual = FaseTurno.ColocacionInicial;
        }

        public bool AvanzarFase()
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
             
                    if (JugadorActual.ManoCartas.Count >= 6)
                    {
                        MessageBox.Show($"No puedes pasar a la fase de ataque. Tienes {JugadorActual.ManoCartas.Count} cartas.\nDebes intercambiar al menos un trío de cartas.",
                                      "Intercambio Obligatorio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                    FaseActual = FaseTurno.Ataque;
                    break;

                case FaseTurno.Ataque:
                    FaseActual = FaseTurno.Planeacion;
                    break;
            }

            if (VerificarVictoria())
            {
                MessageBox.Show($"¡{JugadorActual.Nombre} ha conquistado el mundo!\n¡Felicidades, has ganado el juego!",
                              "Victoria", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return true; 
        }

        private bool EsJugadorActivo(Jugador j)
        {
            
            if (EjercitoNeutral == null)
                return j.TerritoriosControlados.Count > 0;

            
            return j != EjercitoNeutral && j.TerritoriosControlados.Count > 0;
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
            
            bool modoTresJugadores = (EjercitoNeutral == null);

            if (modoTresJugadores)
            {
              
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
                        Jugadores.Obtener(2).AgregarTerritorio(arrayTerritorios[i]);
                        arrayTerritorios[i].CambiarOcupante(Jugadores.Obtener(2));
                    }
                    arrayTerritorios[i].AgregarTropas(1);
                }
            }
            else
            {
               
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
            } 
        }

        private void SiguienteJugadorColocacion()
        {
         
            if (TodosJugadoresTerminaron())
            {
                FaseActual = FaseTurno.Refuerzo;

                for (int i = 0; i < Jugadores.Count; i++)
                {
                    Jugador jugador = Jugadores.Obtener(i);
                    if (EsJugadorActivo(jugador))
                    {
                        turnoActual = i;
                        JugadorActual = jugador;
                        DarRefuerzosAlJugador();
                        return;
                    }
                }
                return;
            }
            
            int intentos = 0;
            int indiceInicial = turnoActual;

            do
            {
                turnoActual = (turnoActual + 1) % Jugadores.Count;
                JugadorActual = Jugadores.Obtener(turnoActual);
                intentos++;

                if (JugadorActual.TropasDisponibles > 0)
                {
                
                    if (JugadorActual == EjercitoNeutral)
                    {
                        ColocarTropaNeutralAutomatica();

                        if (EjercitoNeutral.TropasDisponibles <= 0)
                        {
                     
                            FaseActual = FaseTurno.Refuerzo;
                            for (int i = 0; i < Jugadores.Count; i++)
                            {
                                Jugador jugador = Jugadores.Obtener(i);
                                if (EsJugadorActivo(jugador))
                                {
                                    turnoActual = i;
                                    JugadorActual = jugador;
                                    DarRefuerzosAlJugador();
                                    return;
                                }
                            }
                        }
                        intentos = 0;
                        indiceInicial = turnoActual;
                    }
                    else
                    {
                        break; 
                    }
                }
            }
            while (intentos < Jugadores.Count);
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

        public bool VerificarVictoria()
        {
           
            for (int i = 0; i < Jugadores.Count; i++)
            {
                Jugador jugador = Jugadores.Obtener(i);

                if (jugador == EjercitoNeutral)
                    continue;
         
                if (jugador.TerritoriosControlados.Count == TOTAL_TERRITORIOS)
                {
                    JuegoTerminado = true;
                    return true;
                }
            }
            return false;
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
     
            if (JugadorActual.ManoCartas.Count >= 6)
            {
                MessageBox.Show($"Tienes {JugadorActual.ManoCartas.Count} cartas. Debes intercambiar al menos un trío antes de poder atacar.",
                              "Intercambio Obligatorio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public bool PuedeIntercambiarCartas()
        {
         
            return JugadorActual.ManoCartas.Count >= 3;
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
            Territorio territorioAleatorio = EjercitoNeutral.TerritoriosControlados.Obtener(randomIndex);
 
            territorioAleatorio.AgregarTropas(1);
            EjercitoNeutral.TropasDisponibles--;
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
        public bool ColocarTropaInicial(Territorio territorio)
        {
            if (FaseActual != FaseTurno.ColocacionInicial ||
                JugadorActual.TropasDisponibles <= 0 ||
                !TerritorioPerteneceAJugadorActual(territorio))
            {
                return false;
            }

            territorio.AgregarTropas(1);
            JugadorActual.TropasDisponibles--;
        
            SiguienteJugadorColocacion();

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
          
            if (VerificarVictoria())
            {
                MessageBox.Show($"¡{JugadorActual.Nombre} ha conquistado el mundo!\n¡Felicidades, has ganado el juego!",
                              "Victoria", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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