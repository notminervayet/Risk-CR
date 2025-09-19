using System;
using System.Drawing;
using System.Windows.Forms;

namespace Risk_CR
{
    public class Territorio
    {
        private static Random rnd = new Random();

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Provincia { get; set; }
        public object Ocupante { get; set; }
        public int Tropas { get; set; }
        public bool TieneCarta { get; set; }
        public Button BotonAsociado { get; set; }
        public ListaGod<Territorio> TerritoriosAdyacentes { get; set; } 

        public Territorio(int id, string nombre, string provincia)
        {
            Id = id;
            Nombre = nombre;
            Provincia = provincia;
            Tropas = 0;
            Ocupante = null;
            BotonAsociado = null;
            TieneCarta = true;
            TerritoriosAdyacentes = new ListaGod<Territorio>();
        }

        public void AgregarAdyacente(Territorio territorioAdyacente)
        {
            if (!TerritoriosAdyacentes.Contiene(territorioAdyacente)) 
            {
                TerritoriosAdyacentes.Agregar(territorioAdyacente); 
            }
        }

        public void ActualizarVisualmente()
        {
            if (BotonAsociado != null)
            {
                BotonAsociado.Text = $"{Nombre}\n{Tropas}";

                if (Ocupante == null)
                {
                    BotonAsociado.BackColor = Color.LightGray;
                }
                else
                {
                    BotonAsociado.BackColor = Color.LightBlue;
                }

                BotonAsociado.ForeColor = Color.Black;
                BotonAsociado.Font = new Font("Arial", 8, FontStyle.Bold);
                BotonAsociado.TextAlign = ContentAlignment.MiddleCenter;
            }
        }

        public void AgregarTropas(int cantidad)
        {
            Tropas += cantidad;
            ActualizarVisualmente();
        }

        public void RemoverTropas(int cantidad)
        {
            Tropas = Math.Max(0, Tropas - cantidad);
            ActualizarVisualmente();
        }

        public void CambiarOcupante(object nuevoOcupante)
        {
            Ocupante = nuevoOcupante;
            ActualizarVisualmente();
        }

        public bool PuedeAtacar()
        {
            return Tropas >= 2;
        }

        public bool EsAdyacente(Territorio otroTerritorio)
        {
            return TerritoriosAdyacentes.Contiene(otroTerritorio);
        }

        public override string ToString()
        {
            return $"#{Id} {Nombre} ({Provincia}) - Tropas: {Tropas} - Adyacentes: {TerritoriosAdyacentes.Count}";
        }

        public string ObtenerNombresAdyacentes()
        {
            ListaGod<string> nombres = new ListaGod<string>(); 
            for (int i = 0; i < TerritoriosAdyacentes.Count; i++) 
            {
                Territorio adyacente = TerritoriosAdyacentes.Obtener(i); 
                nombres.Agregar(adyacente.Nombre); 
            }
            return string.Join(", ", nombres.ConvertirAArray());
        }

        public Carta ReclamarCarta()
        {
            if (TieneCarta)
            {
                TieneCarta = false;
                TipoCarta tipo = (TipoCarta)rnd.Next(0, 3);
                return new Carta(Nombre, tipo);
            }
            return null;
        }
    }
}