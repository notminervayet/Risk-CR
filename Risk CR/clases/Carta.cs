using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Risk_CR.clases
{
    public class Carta
    {
        public int Infanteria { get; set; }
        public int Caballeria { get; set; }
        public int Artilleria { get; set; }

        public Carta(int infanteria, int caballeria, int artilleria)
        {
            Infanteria = infanteria;
            Caballeria = caballeria;
            Artilleria = artilleria;
        }

    }
}
