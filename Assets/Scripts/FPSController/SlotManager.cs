using System.Collections;
using UnityEngine;

public class SlotManager : MonoBehaviour
{
    public GameObject[] slots; // Referencia a los slots
    public Color highlightColor; // Color que los slots cambiarán
    public GameObject activadorNota; // La carta que aparecerá en el centro
    public GameObject canvasEnd; // El Canvas que contendrá la imagen final
    public GameObject imageEnd; // La imagen final (asegúrate de renombrar este GameObject en el Inspector)

    private int slotsFilled = 0;

    private void Start()
    {
        // Asegúrate de que la carta esté oculta al inicio
        activadorNota.SetActive(false);
        // Asegúrate de que la imagen final esté oculta al inicio
        imageEnd.SetActive(false);
    }

    public void UpdateSlotColor(GameObject slot)
    {
        // Verificar si el slot tiene un Renderer
        Renderer slotRenderer = slot.GetComponent<Renderer>();
        if (slotRenderer == null)
        {
            Debug.LogError("El slot no tiene un componente Renderer.");
            return;
        }

        // Crear una nueva instancia del material para este slot en particular
        if (slotRenderer.material != null)
        {
            slotRenderer.material = new Material(slotRenderer.material); // Clonar el material para este slot
            slotRenderer.material.color = highlightColor; // Cambia el color del material clonado
        }

        // Aumenta el contador de slots llenos
        slotsFilled++;

        // Si todos los slots están llenos
        if (slotsFilled >= slots.Length)
        {
            Debug.Log("Todos los slots están llenos. Mostrando la carta.");
            ShowCard();
            StartCoroutine(EndGameSequence());
        }
    }

    private void ShowCard()
    {
        Debug.Log("Mostrando la carta.");
        // Muestra la carta en el centro
        activadorNota.SetActive(true);
        // Desactiva el CanvasEnd si no es necesario mostrarlo aún
        canvasEnd.SetActive(false);
    }

    private IEnumerator EndGameSequence()
    {
        Debug.Log("Iniciando secuencia de fin de juego.");
        yield return new WaitForSeconds(2); // Espera un momento antes de mostrar la imagen final
        ShowEndImage();
    }

    private void ShowEndImage()
    {
        // Activa el CanvasEnd
        canvasEnd.SetActive(true);

        // Asegúrate de que la imagen final esté activada
        imageEnd.SetActive(true);

        // Inicia la animación de la imagen final
        AnimacionImagenFinal animacion = canvasEnd.GetComponent<AnimacionImagenFinal>();
        if (animacion != null)
        {
            StartCoroutine(animacion.AnimarImagenFinal());
        }
        else
        {
            Debug.LogError("No se encontró el componente AnimacionImagenFinal en canvasEnd.");
        }
    }

    // Método opcional si decides usar tags para verificar el tipo de objeto en cada slot.
    public bool AcceptsObject(GameObject obj)
    {
        string[] acceptedTags = { "ImageA", "ImageB", "ImageC", "ImageD" };
        
        // Verifica si el objeto tiene uno de los tags aceptados
        foreach (var tag in acceptedTags)
        {
            if (obj.CompareTag(tag))
            {
                return true; // El objeto es aceptado
            }
        }
        return false; // El objeto no tiene un tag válido
    }
}
