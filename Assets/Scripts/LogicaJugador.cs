using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicaJugador : MonoBehaviour
{
    public GameObject estelaPrefab;
    public GameObject poderPrefab;
    private ListaEnlazada Moto;
    private Vector2Int direccion;
    private Vector2Int comidaPosicion;
    private float tiempoEntreMovimientos = 0.2f;
    private float temporizador;

    void Start()
    {
        Moto = new ListaEnlazada();
        Moto.AgregarAlFinal(new Vector2Int(10, 10));
        direccion = Vector2Int.right;
        GenerarComida();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow)) direccion = Vector2Int.up;
        if (Input.GetKeyDown(KeyCode.DownArrow)) direccion = Vector2Int.down;
        if (Input.GetKeyDown(KeyCode.LeftArrow)) direccion = Vector2Int.left;
        if (Input.GetKeyDown(KeyCode.RightArrow)) direccion = Vector2Int.right;

        temporizador += Time.deltaTime;
        if (temporizador >= tiempoEntreMovimientos)
        {
            temporizador = 0;
            MoverMoto();
        }
    }

    void MoverMoto()
    {
        Vector2Int nuevaPosicion = Moto.Cabeza.Posicion + direccion;
        Moto.AgregarAlFinal(nuevaPosicion);

        if (nuevaPosicion == comidaPosicion)
        {
            GenerarComida();
        }
        else
        {
            Moto.EliminarPrimero();
        }

        DibujarMoto();
    }

    void GenerarComida()
    {
        comidaPosicion = new Vector2Int(Random.Range(0, 20), Random.Range(0, 20));
        Instantiate(poderPrefab, new Vector3(comidaPosicion.x, comidaPosicion.y, 0), Quaternion.identity);
        
    }

    void DibujarMoto()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        Nodo actual = Moto.Cabeza;
        while (actual != null)
        {
            Instantiate(estelaPrefab, new Vector3(actual.Posicion.x, actual.Posicion.y, 0), Quaternion.identity, transform);
            actual = actual.Siguiente;
        }
    }
}
