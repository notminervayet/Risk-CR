using System;
using System.Collections;
using System.Collections.Generic;

namespace Risk_CR
{
    public class ListaGod<T> : IEnumerable<T>
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

        //cuenta de elementos en la lista
        public int Count => cantidad;

        public T Obtener(int index)
        {
            // Verificar que el índice esté dentro de los límites
            if (index < 0 || index >= cantidad)
                throw new IndexOutOfRangeException();
            return elementos[index];
        }

        public void Establecer(int index, T value)
        {
            //cambiar el valor en el índice especificado por otro valor
            if (index < 0 || index >= cantidad)
                throw new IndexOutOfRangeException();
            elementos[index] = value;
        }
        public void Agregar(T item)
        {
            // Si el array está lleno, duplica su tamaño creando uno nuevo y copiando los elementos
            if (cantidad >= capacidad)
            {
                capacidad *= 2;
                T[] nuevoArray = new T[capacidad];
                for (int i = 0; i < cantidad; i++)
                    nuevoArray[i] = elementos[i];
                elementos = nuevoArray;
            }
            elementos[cantidad++] = item;
        }
        public void Add(T item)
        {
            Agregar(item);
        }

        public bool Contiene(T item)
        {
            //revisar si el elemento está en la lista
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
            //eliminar el primer elemento que coincida con el valor especificado
            for (int i = 0; i < cantidad; i++)
            {
                if ((elementos[i] == null && item == null) ||
                    (elementos[i] != null && elementos[i].Equals(item)))
                {
                    EliminarEnIndice(i);
                    return true;
                }
            }
            return false;
        }

        private void EliminarEnIndice(int index)
        {
            //eliminar el elemento en el índice especificado
            for (int i = index; i < cantidad - 1; i++)
                elementos[i] = elementos[i + 1];
            cantidad--;
            elementos[cantidad] = default(T);
        }
        public T[] ConvertirAArray()
        {
            //convertir la lista a un array
            T[] array = new T[cantidad];
            for (int i = 0; i < cantidad; i++)
                array[i] = elementos[i];
            return array;
        }

        public void Limpiar()
        {
            cantidad = 0;
        }
        
        public IEnumerator<T> GetEnumerator()
        {
            //iterador para recorrer la lista
            for (int i = 0; i < cantidad; i++)
                yield return elementos[i];
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
