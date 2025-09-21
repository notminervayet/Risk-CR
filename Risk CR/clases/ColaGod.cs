using System;

namespace Risk_CR
{
    public class ColaGod<T>
    {
        private ListaGod<T> elementos;

        public ColaGod()
        {
            elementos = new ListaGod<T>();
        }

       
        public bool EstaVacia => elementos.Count == 0;

        
        public void Enqueue(T item)
        {
            elementos.Agregar(item);
        }

        
        public T Peek()
        {
            if (EstaVacia)
                throw new InvalidOperationException("La cola está vacía.");
            return elementos.Obtener(0);
        }

       
        public T Dequeue()
        {
            if (EstaVacia)
                throw new InvalidOperationException("La cola está vacía.");

            
            T frente = elementos.Obtener(0);

            
            elementos.Remover(frente);

            return frente;
        }
    }
}
