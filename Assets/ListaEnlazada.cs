using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListaEnlazada
{
    public Nodo Cabeza { get; private set; }
    public Nodo Cola { get; private set; }

    public ListaEnlazada(Vector2Int posicionInicial, int longitudInicial)
    {
        Cabeza = new Nodo(posicionInicial);
        Nodo actual = Cabeza;
        for (int i = 1; i < longitudInicial; i++)
        {
            actual.Siguiente = new Nodo(new Vector2Int(posicionInicial.x - i, posicionInicial.y));
            actual = actual.Siguiente;
        }
        Cola = actual;
    }

    public void Mover(Vector2Int nuevaPosicion, bool crecer)
    {
        Nodo nuevoNodo = new Nodo(nuevaPosicion);
        nuevoNodo.Siguiente = Cabeza;
        Cabeza = nuevoNodo;

        if (!crecer)
        {
            // Eliminar la cola
            Nodo actual = Cabeza;
            while (actual.Siguiente != Cola)
            {
                actual = actual.Siguiente;
            }
            actual.Siguiente = null;
            Cola = actual;
        }
    }
}