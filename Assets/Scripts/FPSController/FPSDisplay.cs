using UnityEngine;
using System.Collections;

public class MostrarFPS : MonoBehaviour
{
    // deltaTime nos ayudará a calcular el tiempo que tarda en renderizarse cada frame
    float tiempoDelta = 0.0f;

    // Este método se ejecuta en cada frame del juego
    void Update()
    {
        // Calcula el tiempo entre frames, suavizando el valor con una interpolación para hacerlo más estable
        tiempoDelta += (Time.deltaTime - tiempoDelta) * 0.1f;
    }

    // OnGUI se encarga de dibujar la interfaz gráfica en pantalla (en este caso, el texto que muestra los FPS)
    void OnGUI()
    {
        // "w" representa el ancho de la pantalla, "h" la altura
        int anchoPantalla = Screen.width, altoPantalla = Screen.height;

        // Estilo gráfico para el texto que se va a mostrar (por ejemplo, alineación y color)
        GUIStyle estilo = new GUIStyle();

        // Se define un rectángulo donde se va a mostrar el texto con las dimensiones correspondientes
        Rect rectangulo = new Rect(0, 0, anchoPantalla, altoPantalla * 2 / 100);
        estilo.alignment = TextAnchor.UpperLeft;  // El texto se alinea en la esquina superior izquierda
        estilo.fontSize = altoPantalla * 2 / 100; // El tamaño de la fuente es el 2% de la altura de la pantalla
        estilo.normal.textColor = new Color(1, 1, 1, 1.0f); // El color del texto será blanco

        // Convierte el tiempo por frame (en milisegundos) y los FPS
        float milisegundos = tiempoDelta * 1000.0f;
        float fps = 1.0f / tiempoDelta;

        // Formatea el texto para mostrar tanto los milisegundos como los FPS
        string texto = string.Format("{0:0.0} ms ({1:0.} fps)", milisegundos, fps);

        // Finalmente, dibuja el texto en la pantalla usando el estilo definido
        GUI.Label(rectangulo, texto, estilo);
    }
}
