using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Risk_CR
{
    public class Territorio
    {
        // Propiedades públicas
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Provincia { get; set; }
        public object Ocupante { get; set; }
        public int Tropas { get; set; }
        public Button BotonAsociado { get; set; }
        public List<Territorio> TerritoriosAdyacentes { get; set; } 

        // Constructor
        public Territorio(int id, string nombre, string provincia)
        {
            Id = id;
            Nombre = nombre;
            Provincia = provincia;
            Tropas = 0; 
            Ocupante = null; 
            BotonAsociado = null;
            TerritoriosAdyacentes = new List<Territorio>(); 
        }

       
        public void AgregarAdyacente(Territorio territorioAdyacente)
        {
            if (!TerritoriosAdyacentes.Contains(territorioAdyacente))
            {
                TerritoriosAdyacentes.Add(territorioAdyacente);
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
            return TerritoriosAdyacentes.Contains(otroTerritorio);
        }

       
        public override string ToString()
        {
            return $"#{Id} {Nombre} ({Provincia}) - Tropas: {Tropas} - Adyacentes: {TerritoriosAdyacentes.Count}";
        }

      
        public string ObtenerNombresAdyacentes()
        {
            List<string> nombres = new List<string>();
            foreach (Territorio adyacente in TerritoriosAdyacentes)
            {
                nombres.Add(adyacente.Nombre);
            }
            return string.Join(", ", nombres);
        }
    }
}