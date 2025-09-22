using System;

namespace Risk_CR
{
    public class Dado
    {
        private static readonly Random rng = new Random();

        public ListaGod<int> AtacanteTiradas { get; private set; }
        public ListaGod<int> DefensorTiradas { get; private set; }

        public int EjercitosPerdidosAtacante { get; private set; }
        public int EjercitosPerdidosDefensor { get; private set; }
         
        public Dado()
        {
            AtacanteTiradas = new ListaGod<int>();
            DefensorTiradas = new ListaGod<int>();
        }

        public void LanzarYComparar(int dadosAtacante, int dadosDefensor)
        {
            if (dadosAtacante < 1 || dadosAtacante > 3)
                throw new ArgumentOutOfRangeException(nameof(dadosAtacante), "El atacante debe lanzar 1-3 dados.");
            if (dadosDefensor < 1 || dadosDefensor > 2)
                throw new ArgumentOutOfRangeException(nameof(dadosDefensor), "El defensor debe lanzar 1-2 dados.");

            AtacanteTiradas = LanzarDados(dadosAtacante);
            DefensorTiradas = LanzarDados(dadosDefensor);

            CompararTiradas();
        }

        private ListaGod<int> LanzarDados(int cantidad)
        {
            var tiradas = new ListaGod<int>();
            for (int i = 0; i < cantidad; i++)
                tiradas.Agregar(rng.Next(1, 7));

            OrdenarDescendente(tiradas);
            return tiradas;
        }

        private void OrdenarDescendente(ListaGod<int> lista)
        {
            int n = lista.Count;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    if (lista.Obtener(j) > lista.Obtener(i))
                    {
                        int temp = lista.Obtener(i);
                        lista.Establecer(i, lista.Obtener(j));
                        lista.Establecer(j, temp);
                    }
                }
            }
        }

        private void CompararTiradas()
        {
            EjercitosPerdidosAtacante = 0;
            EjercitosPerdidosDefensor = 0;

            int rondas = AtacanteTiradas.Count < DefensorTiradas.Count
                         ? AtacanteTiradas.Count
                         : DefensorTiradas.Count;

            for (int i = 0; i < rondas; i++)
            {
                int ataque = AtacanteTiradas.Obtener(i);
                int defensa = DefensorTiradas.Obtener(i);

                if (ataque > defensa)
                {
                    EjercitosPerdidosDefensor++;
                }
                else if (ataque < defensa)
                {
                    EjercitosPerdidosAtacante++;
                }
                else //(ataque==defensa)
                {
                    //nah
                }

            }
        }
    }
}
