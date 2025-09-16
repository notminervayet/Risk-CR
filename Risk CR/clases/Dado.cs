using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Risk_CR.clases
{
    public class Dado
    {
        private static Random random = new Random();
        public int Lanzar()
        {
            return random.Next(1, 7); 
        }
    }
}
