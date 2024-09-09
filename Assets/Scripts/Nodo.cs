using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nodo : MonoBehaviour
{
    
    public Vector2Int Posicion { get; set; }
    public Nodo Siguiente { get; set; }

    public Nodo(Vector2Int posicion)
        {
        Posicion = posicion;
        Siguiente = null;
        }
    
}
