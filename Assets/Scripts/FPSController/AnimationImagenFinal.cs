using System.Collections; // Asegúrate de incluir esto
using UnityEngine;

public class AnimacionImagenFinal : MonoBehaviour
{
    public float animationDuration = 2f; // Duración de la animación

    // Método para iniciar la animación de la imagen final
    public IEnumerator AnimarImagenFinal() // Asegúrate de que sea público
    {
        // Aquí puedes implementar la lógica de animación, por ejemplo, desvanecer la imagen
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        
        if (canvasGroup != null)
        {
            // Asegúrate de que la imagen esté completamente invisible al principio
            canvasGroup.alpha = 0f;

            // Gradualmente desvanecer la imagen
            float elapsedTime = 0f;
            while (elapsedTime < animationDuration)
            {
                elapsedTime += Time.deltaTime;
                canvasGroup.alpha = Mathf.Clamp01(elapsedTime / animationDuration); // Ajusta la transparencia
                yield return null; // Espera un frame
            }

            // Asegúrate de que esté completamente visible al final
            canvasGroup.alpha = 1f;
        }
    }
}
