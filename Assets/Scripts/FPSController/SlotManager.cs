using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotManager : MonoBehaviour
{
    public Slot[] slots; // Array de slots

    // Método que se llamará cuando todos los slots estén ocupados
    public void AllSlotsFilled()
    {
        // Aquí puedes manejar la lógica que deseas realizar cuando todos los slots estén ocupados
        GameManager.Instance.ShowEndImage(); // Mostrar la imagen de finalización
    }

    // Método para verificar si todos los slots están ocupados
    public void CheckSlots()
    {
        foreach (Slot slot in slots)
        {
            if (slot.currentImage == null) // Si hay algún slot vacío
            {
                return; // Salir del método
            }
        }

        AllSlotsFilled(); // Si todos los slots están ocupados
    }
}
