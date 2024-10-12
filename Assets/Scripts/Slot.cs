using UnityEngine;

public class Slot : MonoBehaviour
{
    public string acceptedObjectTag; // El nombre del objeto que el slot acepta.
    public Color highlightColor; // Color que se aplicará cuando el objeto correcto esté en el slot.

    private bool isFilled = false;

    // Verifica si el objeto que entra es aceptado por el slot.
    public bool AcceptsObject(GameObject obj)
    {
        // Compara el nombre del objeto con el valor aceptado
        if (obj.name == acceptedObjectTag && !isFilled)
        {
            return true; // El objeto es aceptado.
        }
        else
        {
            Debug.Log("El objeto no coincide o el slot ya está lleno.");
            return false; // El objeto no es aceptado.
        }
    }

    // Cambia el color del slot y lo marca como lleno.
    public void PlaceObject(GameObject obj)
    {
        if (!isFilled)
        {
            isFilled = true;
            GetComponent<Renderer>().material.color = highlightColor; // Cambia el color del slot.
            Debug.Log("Objeto colocado correctamente: " + obj.name);
            Destroy(obj); // Elimina el objeto una vez colocado.
        }
    }
}
