using System.Collections;
using UnityEngine;

public class SlotManager : MonoBehaviour
{
    public GameObject[] slots; // Referencia a los slots
    public Color highlightColor; // Color que los slots cambiarán
    public GameObject activadorNota6; // La carta que aparecerá en el centro
    public GameObject canvasEnd; // El Canvas que contendrá la imagen final
    public GameObject imageEnd; // La imagen final (asegúrate de renombrar este GameObject en el Inspector)

    private int slotsFilled = 0;

    private void Start()
    {
        // Asegúrate de que la carta esté oculta al inicio
        activadorNota6.SetActive(false);
        // Asegúrate de que la imagen final esté oculta al inicio
        imageEnd.SetActive(false);
    }

    public void UpdateSlotColor(GameObject slot)
    {
        // Cambia el color del slot al color de resaltado
        slot.GetComponent<Renderer>().material.color = highlightColor;

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
        activadorNota6.SetActive(true);
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

        // Asegúrate de que la imagen final esté desactivada al inicio
        imageEnd.SetActive(true); // Asegúrate de que esto esté activado si es necesario

        // Inicia la animación de la imagen final
        AnimacionImagenFinal animacion = canvasEnd.GetComponent<AnimacionImagenFinal>();
        if (animacion != null)
        {
            StartCoroutine(animacion.AnimarImagenFinal());
        }
    }
}
