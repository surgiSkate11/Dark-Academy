using UnityEngine;

public class Slot : MonoBehaviour
{
    private SlotManager slotManager;

    // Asignamos la referencia al SlotManager
    void Start()
    {
        slotManager = FindObjectOfType<SlotManager>();
    }

    // Cuando un objeto con el tag "Imagen" entra en el slot
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Imagen"))
        {
            slotManager.AddObjectToSlot();
        }
    }

    // Cuando un objeto con el tag "Imagen" sale del slot
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Imagen"))
        {
            slotManager.RemoveObjectFromSlot();
        }
    }
}
