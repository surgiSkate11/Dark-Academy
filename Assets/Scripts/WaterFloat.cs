using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlotarEnAgua : MonoBehaviour
{

    // Esta variable define la altura a la cual el objeto debería flotar en el agua
    public float AlturaDelAgua = 15.5f;

    // El método "Start" se ejecuta una vez al inicio cuando el objeto es creado o activado
    // En este caso no estamos inicializando nada en este método
    void Start()
    {
        // Código de inicialización (si fuera necesario)
    }

    // El método "Update" se ejecuta una vez por cada frame (fotograma) del juego
    void Update()
    {

        // Verificamos si la posición del objeto (en el eje Y) está por debajo del nivel del agua
        if (transform.position.y < AlturaDelAgua)
        {

            // Si el objeto está debajo del nivel del agua, ajustamos su posición para que flote.
            // Creamos una nueva posición en el mismo punto (x, z) pero con la altura del agua (AlturaDelAgua) en el eje Y.
            transform.position = new Vector3(transform.position.x, AlturaDelAgua, transform.position.z);
        }

    }
}
