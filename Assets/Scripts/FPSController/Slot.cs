using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public GameObject currentImage; // Imagen actualmente en el slot
    public SlotManager slotManager; // Referencia al SlotManager

    private void Start()
    {
        slotManager = FindObjectOfType<SlotManager>(); // Encontrar el SlotManager
    }

    // Método para colocar la imagen en el slot
    public void PlaceImage(GameObject image)
    {
        // Si el slot está vacío
        if (currentImage == null)
        {
            currentImage = image;
            image.transform.SetParent(transform); // Hacer que la imagen sea hija del slot
            image.transform.localPosition = Vector3.zero; // Ajustar la posición de la imagen
            image.GetComponent<Collider>().isTrigger = true; // Desactivar el collider de la imagen
            image.GetComponent<Rigidbody>().isKinematic = true; // Evitar que la imagen caiga

            // Verificar si todos los slots están ocupados
            slotManager.CheckSlots(); // Notificar al SlotManager
        }
    }

    // Método para liberar el slot si es necesario
    public void ReleaseImage()
    {
        if (currentImage != null)
        {
            currentImage.transform.SetParent(null); // Separar la imagen del slot
            currentImage.GetComponent<Collider>().isTrigger = false; // Reactivar el collider
            currentImage.GetComponent<Rigidbody>().isKinematic = false; // Restaurar la física
            currentImage = null; // Limpiar el slot
        }
    }
}
