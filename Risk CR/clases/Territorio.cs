using System;
using System.Drawing;
using System.Windows.Forms;

namespace Risk_CR  // ← Quita el ".clases" 
{
    public class Territorio  // ← Cambia "internal" por "public"
    {
        // Agrega estas propiedades:
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Provincia { get; set; }
        public int Tropas { get; set; }
        public object Ocupante { get; set; }
        public Button BotonAsociado { get; set; }

        // Agrega este constructor:
        public Territorio(int id, string nombre, string provincia)
        {
            Id = id;
            Nombre = nombre;
            Provincia = provincia;
            Tropas = 0;
            Ocupante = null;
            BotonAsociado = null;
        }

        // Agrega este método:
        public void ActualizarVisualmente()
        {
            if (BotonAsociado != null)
            {
                BotonAsociado.Text = $"{Nombre}\n{Tropas}";
                BotonAsociado.BackColor = Color.LightGray;
            }
        }
    }
}