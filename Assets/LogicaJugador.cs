using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    public GameObject snakeSegmentPrefab;
    public GameObject foodPrefab;
    public GameObject aiSnakePrefab;
    public UnityEngine.UI.Text enemyCounterText; // Texto UI para mostrar el contador
    private ListaEnlazada serpiente;
    private Vector2Int direccion;
    private Vector2Int comidaPosicion;
    private float tiempoEntreMovimientos = 0.2f;
    private float temporizador;
    private List<AISnakeController> aiSnakes;
    private bool crecer;
    private int enemigosEliminados;
    private float tiempoParaNuevaSerpiente = 5f; // Tiempo entre la aparición de nuevas serpientes
    private float temporizadorNuevaSerpiente;
    private int ancho = 110;
    private int alto = 50;
    private GameObject NewComida;
    private float randomNumber;
    private int tamaño = 3;


    void Start()
    {
        serpiente = new ListaEnlazada(new Vector2Int(10, 10), 3);
        direccion = Vector2Int.right;
        GenerarComida();
        aiSnakes = new List<AISnakeController>();
        crecer = false;
        enemigosEliminados = 0;
        ActualizarContadorEnemigos();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            if (direccion != Vector2Int.up)
            {
                direccion = Vector2Int.up;
            }
        }

        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            if (direccion != Vector2Int.down)
            {
                direccion = Vector2Int.down;
            }
        }

        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            if (direccion != Vector2Int.left)
            {
                direccion = Vector2Int.left;
            }
        }

        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            if (direccion != Vector2Int.right)
            {
                direccion = Vector2Int.right;
            }
        }
    

        temporizador += Time.deltaTime;
        temporizadorNuevaSerpiente += Time.deltaTime;

        if (temporizador >= tiempoEntreMovimientos)
        {
            temporizador = 0;
            MoverSerpiente();
        }

        if (temporizadorNuevaSerpiente >= tiempoParaNuevaSerpiente && aiSnakes.Count < 4)
        {
            temporizadorNuevaSerpiente = 0;
            GenerarAISnake();
        }
    }

    void MoverSerpiente()
    {
        Vector2Int nuevaPosicion = serpiente.Cabeza.Posicion + direccion;

        // Verificar colisión con el borde
        if (nuevaPosicion.x < 0 || nuevaPosicion.x >= ancho || nuevaPosicion.y < 0 || nuevaPosicion.y >= alto)
        {
            // Terminar el juego si el jugador toca el borde
            Debug.Log("¡Juego terminado! La serpiente tocó el borde.");
            Destroy(gameObject);
            return;
        }

        serpiente.Mover(nuevaPosicion, crecer);

        if (nuevaPosicion == comidaPosicion)
        {
            Destroy(NewComida);
            crecer = true;
            GenerarComida();
            tamaño += 1;
        }
        else
        {
            crecer = false;
        }

        DibujarSerpiente();
        VerificarColisiones();
    }

    void GenerarComida()
    {
        comidaPosicion = new Vector2Int(Random.Range(5, 105), Random.Range(5, 45));
        NewComida = Instantiate(foodPrefab, new Vector3(comidaPosicion.x, comidaPosicion.y, 0), Quaternion.identity);
    }

    void GenerarAISnake()
    {
        GameObject aiSnake = Instantiate(aiSnakePrefab, Vector3.zero, Quaternion.identity);
        aiSnakes.Add(aiSnake.GetComponent<AISnakeController>());
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

    void VerificarColisiones()
    {
        Nodo cabeza = serpiente.Cabeza;
        foreach (var aiSnake in aiSnakes)
        {
            Nodo actual = aiSnake.GetComponent<AISnakeController>().serpiente.Cabeza;
            bool colisionConCabeza = false;

            while (actual != null)
            {
                if (cabeza.Posicion == actual.Posicion)
                {
                    if (actual == aiSnake.GetComponent<AISnakeController>().serpiente.Cabeza)
                    {
                        // Colisión con la cabeza de la serpiente controlada por la computadora
                        Debug.Log("¡Juego terminado! La serpiente tocó la cabeza de una serpiente controlada por la computadora.");
                        Destroy(gameObject);
                        Destroy(aiSnake.gameObject);
                        return;
                    }
                    else
                    {
                        // Colisión con cualquier otra parte de la serpiente controlada por la computadora
                        Destroy(aiSnake.gameObject);
                        aiSnakes.Remove(aiSnake);
                        enemigosEliminados++;
                        ActualizarContadorEnemigos();
                        colisionConCabeza = true;
                        break;
                    }
                }
                actual = actual.Siguiente;
            }

            if (colisionConCabeza)
            {
                break;
            }
        }
    }

    void ActualizarContadorEnemigos()
    {
        enemyCounterText.text = "Enemigos Eliminados: " + enemigosEliminados;
    }
}


