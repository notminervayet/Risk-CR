namespace Risk_CR.Jugadores
{
    public class Jugador
    {
        public string Nombre { get; set; }
        public string Color { get; set; }
        public int TropasDisponibles { get; set; }
        public ListaGod<Territorio> TerritoriosControlados { get; set; } 
        public ListaGod<Carta> ManoCartas { get; private set; } 

        public Jugador(string nombre, string color)
        {
            Nombre = nombre;
            Color = color;
            TropasDisponibles = 40;
            TerritoriosControlados = new ListaGod<Territorio>(); 
            ManoCartas = new ListaGod<Carta>(); 
        }

        public void AgregarTerritorio(Territorio territorio)
        {
            //si no contiene territoriose agrega
            if (!TerritoriosControlados.Contiene(territorio)) 
            {
                TerritoriosControlados.Agregar(territorio); 
                territorio.Ocupante = this;
            }
        }

        public void RemoverTerritorio(Territorio territorio)
        {
            //quita al actual jugador del territorio
            if (TerritoriosControlados.Contiene(territorio)) 
            {
                TerritoriosControlados.Remover(territorio);
                territorio.Ocupante = null;
            }
        }

        public void ReforzarTropas(int cantidad)
        {
            TropasDisponibles += cantidad;
        }

        public void DesplegarTropas(Territorio territorio, int cantidad)
        {
            //segun las validaciones pone tropas y resta de las que tenia
            if (TropasDisponibles >= cantidad && TerritoriosControlados.Contiene(territorio))
            {
                territorio.Tropas += cantidad;
                TropasDisponibles -= cantidad;
                territorio.ActualizarVisualmente();
            }
        }

        public void RecibirCarta(Carta carta)
        {
            ManoCartas.Agregar(carta); 
        }

        public void EntregarCartas(ListaGod<Carta> cartas) 
        {
            for (int i = 0; i < cartas.Count; i++) 
            {
                Carta carta = cartas.Obtener(i); 
                ManoCartas.Remover(carta); 
            }
        }
    }
}