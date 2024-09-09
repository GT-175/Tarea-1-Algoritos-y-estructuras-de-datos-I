using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListaEnlazada : MonoBehaviour
{
    public Nodo Cabeza { get; private set; }

    public void AgregarAlFinal(Vector2Int posicion)
    {
        Nodo nuevoNodo = new Nodo(posicion);
        if (Cabeza == null)
        {
            Cabeza = nuevoNodo;
        }
        else
        {
            Nodo actual = Cabeza;
            while (actual.Siguiente != null)
            {
                actual = actual.Siguiente;
            }
            actual.Siguiente = nuevoNodo;
        }
    }

    public void EliminarPrimero()
    {
        if (Cabeza != null)
        {
            Cabeza = Cabeza.Siguiente;
        }
    }
}
