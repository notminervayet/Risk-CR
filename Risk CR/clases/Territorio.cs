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
        public object Ocupante { get; set; } // Temporal: luego será Jugador
        public int Tropas { get; set; }
        public Button BotonAsociado { get; set; }
        public List<Territorio> TerritoriosAdyacentes { get; set; } // ← NUEVA PROPIEDAD

        // Constructor
        public Territorio(int id, string nombre, string provincia)
        {
            Id = id;
            Nombre = nombre;
            Provincia = provincia;
            Tropas = 1; // Empieza con 1 tropa
            Ocupante = null; // Neutral al inicio
            BotonAsociado = null;
            TerritoriosAdyacentes = new List<Territorio>(); // ← INICIALIZAR LISTA
        }

        // Método para agregar territorio adyacente ← NUEVO MÉTODO
        public void AgregarAdyacente(Territorio territorioAdyacente)
        {
            if (!TerritoriosAdyacentes.Contains(territorioAdyacente))
            {
                TerritoriosAdyacentes.Add(territorioAdyacente);
            }
        }

        // Método para actualizar la apariencia visual del botón
        public void ActualizarVisualmente()
        {
            if (BotonAsociado != null)
            {
                // Actualizar texto con nombre y tropas
                BotonAsociado.Text = $"{Nombre}\n{Tropas}";

                // Actualizar color según el ocupante
                if (Ocupante == null)
                {
                    BotonAsociado.BackColor = Color.LightGray; // Neutral
                }
                else
                {
                    // Cuando tengas la clase Jugador, aquí usarás:
                    // BotonAsociado.BackColor = ((Jugador)Ocupante).Color;
                    BotonAsociado.BackColor = Color.LightBlue; // Temporal
                }

                // Mejorar apariencia visual
                BotonAsociado.ForeColor = Color.Black;
                BotonAsociado.Font = new Font("Arial", 8, FontStyle.Bold);
                BotonAsociado.TextAlign = ContentAlignment.MiddleCenter;
            }
        }

        // Método para agregar tropas
        public void AgregarTropas(int cantidad)
        {
            Tropas += cantidad;
            ActualizarVisualmente(); // Actualiza automáticamente el botón
        }

        // Método para remover tropas
        public void RemoverTropas(int cantidad)
        {
            Tropas = Math.Max(0, Tropas - cantidad); // No menos de 0
            ActualizarVisualmente();
        }

        // Método para cambiar ocupante
        public void CambiarOcupante(object nuevoOcupante)
        {
            Ocupante = nuevoOcupante;
            ActualizarVisualmente();
        }

        // Método para verificar si puede atacar (mínimo 2 tropas)
        public bool PuedeAtacar()
        {
            return Tropas >= 2;
        }

        // Método para verificar si es adyacente a otro territorio
        public bool EsAdyacente(Territorio otroTerritorio)
        {
            return TerritoriosAdyacentes.Contains(otroTerritorio);
        }

        // Override de ToString para debugging
        public override string ToString()
        {
            return $"#{Id} {Nombre} ({Provincia}) - Tropas: {Tropas} - Adyacentes: {TerritoriosAdyacentes.Count}";
        }

        // Método para obtener nombres de adyacentes (útil para debugging)
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