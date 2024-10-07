using UnityEngine;
using UnityEngine.UI;  // Necesario para usar UI Image

public class SlotManager : MonoBehaviour
{
    public Slot[] slots;              // Array de todos los slots
    public GameObject completionImage; // La imagen o GameObject que se mostrará al completar

    void Start()
    {
        // Asegúrate de que la imagen esté desactivada al inicio
        if (completionImage != null)
        {
            completionImage.SetActive(false); // Desactiva la imagen al inicio
        }
    }

    void Update()
    {
        // Si todos los slots están llenos, muestra la imagen
        if (AllSlotsFilled())
        {
            Debug.Log("¡Todos los objetos están correctamente colocados!");
            ShowCompletionImage();
        }
    }

    private bool AllSlotsFilled()
    {
        // Verificar si todos los slots están llenos
        foreach (Slot slot in slots)
        {
            if (!slot.IsSlotFilled())
            {
                return false; // Si al menos un slot está vacío, devuelve false
            }
        }
        return true; // Si todos los slots están llenos, devuelve true
    }

    private void ShowCompletionImage()
    {
        if (completionImage != null && !completionImage.activeInHierarchy)
        {
            // Activa la imagen si no está ya activa
            completionImage.SetActive(true);
        }
    }
}
