using UnityEngine;

public class Slot : MonoBehaviour
{
    public string requiredTag = "Imagen"; // Tag que debe coincidir con el objeto que va en este slot
    private bool isSlotFilled = false;    // Para verificar si el slot ya está lleno

    void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto que entra tiene el tag correcto
        if (other.CompareTag(requiredTag) && !isSlotFilled)
        {
            isSlotFilled = true;  // Marcar el slot como lleno
            Debug.Log("Objeto colocado correctamente en el slot.");
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Cuando se retira el objeto, el slot se vuelve a liberar
        if (other.CompareTag(requiredTag) && isSlotFilled)
        {
            isSlotFilled = false; // El slot se vacía
            Debug.Log("El objeto fue removido del slot.");
        }
    }

    public bool IsSlotFilled()
    {
        return isSlotFilled;
    }
}
