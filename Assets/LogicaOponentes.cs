using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISnakeController : MonoBehaviour
{
    public GameObject snakeSegmentPrefab;
    public ListaEnlazada serpiente;
    private Vector2Int direccion;
    private float tiempoEntreMovimientos = 0.3f;
    private float temporizador;
    private Vector2Int[] direcciones = { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };
    private System.Random random;
    private bool crecer;
    private int ancho = 110;
    private int alto = 50;

    void Start()
    {
        serpiente = new ListaEnlazada(new Vector2Int(Random.Range(1, ancho - 1), Random.Range(1, alto - 1)), 3);
        direccion = direcciones[Random.Range(0, direcciones.Length)];
        random = new System.Random();
        crecer = false;
    }

    void Update()
    {
        temporizador += Time.deltaTime;
        if (temporizador >= tiempoEntreMovimientos)
        {
            temporizador = 0;
            MoverSerpiente();
        }
    }

    void MoverSerpiente()
    {
        Vector2Int nuevaPosicion = serpiente.Cabeza.Posicion + direccion;

        // Verificar colisión con el borde
        if (nuevaPosicion.x < 0 || nuevaPosicion.x >= ancho || nuevaPosicion.y < 0 || nuevaPosicion.y >= alto)
        {
            // Eliminar la serpiente controlada por la computadora si toca el borde
            Destroy(gameObject);
            return;
        }

        serpiente.Mover(nuevaPosicion, crecer);

        // Cambiar dirección aleatoriamente
        if (random.NextDouble() < 0.1)
        {
            direccion = direcciones[Random.Range(0, direcciones.Length)];
        }

        DibujarSerpiente();
    }

    void DibujarSerpiente()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        Nodo actual = serpiente.Cabeza;
        while (actual != null)
        {
            Instantiate(snakeSegmentPrefab, new Vector3(actual.Posicion.x, actual.Posicion.y, 0), Quaternion.identity, transform);
            actual = actual.Siguiente;
        }
    }
}
