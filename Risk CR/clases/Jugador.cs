using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Risk_CR.clases
{
    public class Jugador
    {
        public string Nombre { get; set; }
        public string Color { get; set; }
        public int TropasDisponibles { get; set; }
        public List<Territorio> TerritoriosControlados { get; set; }
        public Jugador(string nombre, string color)
        {
            Nombre = nombre;
            Color = color;
            TropasDisponibles = 40;
            TerritoriosControlados = new List<Territorio>();
        }
        public void AgregarTerritorio(Territorio territorio)
        {
            if (!TerritoriosControlados.Contains(territorio))
            {
                TerritoriosControlados.Add(territorio);
                territorio.Ocupante = this;
            }
        }
        public void RemoverTerritorio(Territorio territorio)
        {
            if (TerritoriosControlados.Contains(territorio))
            {
                TerritoriosControlados.Remove(territorio);
                territorio.Ocupante = null;
            }
        }
        public void ReforzarTropas(int cantidad)
        {
            TropasDisponibles += cantidad;
        }
        public void DesplegarTropas(Territorio territorio, int cantidad)
        {
            if (TropasDisponibles >= cantidad && TerritoriosControlados.Contains(territorio))
            {
                territorio.Tropas += cantidad;
                TropasDisponibles -= cantidad;
                territorio.ActualizarVisualmente();
            }
        }
    }
}
