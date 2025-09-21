using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Risk_CR
{
    public enum TipoCarta
    {
        Infanteria,
        Caballeria,
        Artilleria,
    }

    public class Carta
    {
        public string Territorio { get; set; }
        public TipoCarta Tipo { get; set; }

        public Carta(string territorio, TipoCarta tipo)
        {
            Territorio = territorio;
            Tipo = tipo;
        }

       
    }
}
