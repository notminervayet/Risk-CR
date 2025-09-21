using Risk_CR.Jugadores;
using System;

namespace Risk_CR
{
    public class Juego
    {
   
        private static Juego _instance;
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

        public enum FaseTurno { ColocacionInicial, Refuerzo, Ataque, Planeacion }


        private Juego()
        {
            Jugadores = new ListaGod<Jugador>();
            Territorios = new ListaGod<Territorio>();
            FaseActual = FaseTurno.ColocacionInicial;
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

            DistribuirTerritorios();

            for (int i = 0; i < Jugadores.Count; i++)
            {
                if (Jugadores.Obtener(i) != EjercitoNeutral)
                {
                    JugadorActual = Jugadores.Obtener(i);
                    break;
                }
            }

            FaseActual = FaseTurno.ColocacionInicial;
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
         
                if (Jugadores.Obtener(j) != EjercitoNeutral)
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

           
            if (JugadorActual.TropasDisponibles == 0)
            {
                SiguienteJugadorColocacion();
            }

            return true;
        }

        private void SiguienteJugadorColocacion()
        {
            int indiceActual = 0;

            for (int i = 0; i < Jugadores.Count; i++)
            {
                if (Jugadores.Obtener(i) == JugadorActual)
                {
                    indiceActual = i;
                    break;
                }
            }

           
            int siguienteIndice = (indiceActual + 1) % Jugadores.Count;
            while (Jugadores.Obtener(siguienteIndice) == EjercitoNeutral)
            {
                siguienteIndice = (siguienteIndice + 1) % Jugadores.Count;
            }

            JugadorActual = Jugadores.Obtener(siguienteIndice);

          
            if (TodosHumanosTerminaron())
            {
                
                ColocarTropasNeutral();
                FaseActual = FaseTurno.Refuerzo;
            }
        }

        private bool TodosHumanosTerminaron()
        {
            for (int i = 0; i < Jugadores.Count; i++)
            {
                Jugador jugador = Jugadores.Obtener(i);
                if (jugador != EjercitoNeutral && jugador.TropasDisponibles > 0)
                {
                    return false;
                }
            }
            return true;
        }

        private void ColocarTropasNeutral()
        {
            Random rnd = new Random();
            while (EjercitoNeutral.TropasDisponibles > 0)
            {
       
                int randomIndex = rnd.Next(EjercitoNeutral.TerritoriosControlados.Count);
                Territorio territorio = EjercitoNeutral.TerritoriosControlados.Obtener(randomIndex);

                territorio.AgregarTropas(1);
                EjercitoNeutral.TropasDisponibles--;
            }
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
    }
}