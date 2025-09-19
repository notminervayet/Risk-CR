using System;

namespace Risk_CR
{
    public class ListaGod<T>
    {
        private T[] elementos;
        private int capacidad;
        private int cantidad;

        public ListaGod()
        {
            capacidad = 10;
            elementos = new T[capacidad];
            cantidad = 0;
        }

        public int Count
        {
            get { return cantidad; }
        }

        public T Obtener(int index)
        {
            if (index < 0 || index >= cantidad)
                throw new IndexOutOfRangeException();
            return elementos[index];
        }

        public void Establecer(int index, T value)
        {
            if (index < 0 || index >= cantidad)
                throw new IndexOutOfRangeException();
            elementos[index] = value;
        }

        public void Agregar(T item)
        {
            if (cantidad >= capacidad)
            {
                capacidad *= 2;
                T[] nuevoArray = new T[capacidad];
                for (int i = 0; i < cantidad; i++)
                {
                    nuevoArray[i] = elementos[i];
                }
                elementos = nuevoArray;
            }
            elementos[cantidad++] = item;
        }

        public bool Contiene(T item)
        {
            for (int i = 0; i < cantidad; i++)
            {
                if (elementos[i] == null && item == null)
                    return true;
                if (elementos[i] != null && elementos[i].Equals(item))
                    return true;
            }
            return false;
        }

        public bool Remover(T item)
        {
            for (int i = 0; i < cantidad; i++)
            {
                if (elementos[i] == null && item == null)
                {
                    EliminarEnIndice(i);
                    return true;
                }
                if (elementos[i] != null && elementos[i].Equals(item))
                {
                    EliminarEnIndice(i);
                    return true;
                }
            }
            return false;
        }

        private void EliminarEnIndice(int index)
        {
            for (int i = index; i < cantidad - 1; i++)
            {
                elementos[i] = elementos[i + 1];
            }
            cantidad--;
            elementos[cantidad] = default(T);
        }

        public T[] ConvertirAArray()
        {
            T[] array = new T[cantidad];
            for (int i = 0; i < cantidad; i++)
            {
                array[i] = elementos[i];
            }
            return array;
        }

        public void Limpiar()
        {
            cantidad = 0;
        }
    }
}