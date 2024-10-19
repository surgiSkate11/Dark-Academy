using System.Collections;
using UnityEngine;

public class AnimacionImagenFinal : MonoBehaviour
{
    // Variable pública para controlar la duración de la animación, en este caso, es de 2 segundos.
    public float animationDuration = 2f; 
    
    // Referencia pública al componente AudioSource para reproducir la canción de terror.
    public AudioSource audioSource; 

    // Método Start, que se ejecuta cuando el objeto entra en escena.
    void Start()
    {
        // Aquí verifico si el AudioSource ha sido asignado en el inspector.
        // Si existe, desactivo la opción de que la canción comience automáticamente al iniciar el juego.
        if (audioSource != null)
        {
            audioSource.playOnAwake = false; // Desactiva la reproducción automática de la canción.
        }
    }

    // Corrutina que manejará la animación de la imagen final, será pública para poder llamarla desde otros scripts.
    public IEnumerator AnimarImagenFinal()
    {
        // Aquí obtengo el componente CanvasGroup del mismo objeto en el que está este script.
        // CanvasGroup es útil para controlar la opacidad de la imagen de manera suave.
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();

        // Compruebo si el CanvasGroup está correctamente asignado.
        // Si es nulo, significa que no se encontró el componente en este objeto.
        if (canvasGroup == null)
        {
            // Imprimo un mensaje de error en la consola de Unity indicando que falta el CanvasGroup.
            Debug.LogError("El componente CanvasGroup no está asignado o no se encuentra en el GameObject.");
            yield break; // Detengo la ejecución de la corrutina si no hay un CanvasGroup.
        }

        // Me aseguro de que la imagen esté completamente invisible al principio de la animación.
        canvasGroup.alpha = 0f;

        // Variable para llevar el tiempo transcurrido durante la animación.
        float elapsedTime = 0f;

        // Este bucle se ejecuta hasta que la imagen se ha desvanecido completamente en el tiempo especificado (2 segundos).
        while (elapsedTime < animationDuration)
        {
            // Incremento el tiempo transcurrido en cada frame con la diferencia de tiempo entre frames.
            elapsedTime += Time.deltaTime;

            // Ajusto la transparencia (alpha) de la imagen de manera proporcional al tiempo transcurrido.
            canvasGroup.alpha = Mathf.Clamp01(elapsedTime / animationDuration);

            // Hago una pausa en cada frame antes de continuar con la animación en el siguiente frame.
            yield return null;
        }

        // Aseguro que la imagen esté completamente visible al finalizar la animación.
        canvasGroup.alpha = 1f;

        // A continuación, verifico si el AudioSource ha sido correctamente asignado.
        if (audioSource == null)
        {
            // Si no lo ha sido, imprimo un error en la consola de Unity para informar que falta el AudioSource.
            Debug.LogError("El componente AudioSource no está asignado. Por favor, arrastra y suelta el AudioSource en el Inspector.");
            yield break; // Detengo la ejecución de la corrutina si no se ha asignado el AudioSource.
        }

        // Finalmente, reproduzco la canción de terror cuando la imagen está completamente visible.
        audioSource.Play();
    }
}
