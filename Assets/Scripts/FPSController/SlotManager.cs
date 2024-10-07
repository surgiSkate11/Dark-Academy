using UnityEngine;

public class SlotManager : MonoBehaviour
{
    public Slot[] slots; // Array de todos los slots

    void Update()
    {
        if (AllSlotsFilled())
        {
            Debug.Log("¡Todos los objetos están correctamente colocados!");
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
}
