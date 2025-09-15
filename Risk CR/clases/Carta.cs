using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Risk_CR.clases
{
    // Tipo de carta
    public enum TipoUnidad
    {
        Infanteria,
        Caballeria,
        Artilleria
    }

    public class Carta
    {
        public TipoUnidad Unidad { get; private set; }
        public string RutaImagen { get; private set; } 

        public Carta(TipoUnidad unidad, string rutaImagen)
        {
            Unidad = unidad;
            RutaImagen = rutaImagen;
        }
        //muestra el tipo de unidad
        public override string ToString()
        {
            return Unidad.ToString(); 
        }
    }

}
