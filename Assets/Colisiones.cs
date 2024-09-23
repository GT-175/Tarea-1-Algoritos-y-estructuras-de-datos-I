using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Esta clase sirve para que la moto detecte con que choca, y que tiene que hacer la moto del jugador o el objeto en cuestion cuando
//es colisionado por la moto del jugador
public class Colisiones : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //si toca un poder, se quita de la pantalla
        if (collision.gameObject.CompareTag("Enemgo"))
        {
            Debug.Log("Poder adquirido");
            Destroy(collision.gameObject);
        }

        //Si toca uno de los bordes de la pantalla, se acaba el juego
        if (collision.gameObject.CompareTag("leftBorder"))
        {
            Debug.Log("Fin del juego");
            
        }
    }
}

