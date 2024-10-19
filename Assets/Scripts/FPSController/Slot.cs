using UnityEngine;

public class Slot : MonoBehaviour
{
    public string acceptedObjectTag;
    public bool isFilled = false;
    public Color highlightColor;

    public bool AcceptsObject(GameObject obj)
    {
        return !isFilled && obj.tag == acceptedObjectTag;
    }

    public void PlaceObject(GameObject obj)
    {
        if (AcceptsObject(obj))
        {
            isFilled = true;
            Renderer slotRenderer = GetComponent<Renderer>();
            if (slotRenderer == null)
            {
                Debug.LogError("El slot no tiene un componente Renderer.");
                return;
            }

            Material slotMaterial = new Material(slotRenderer.material);
            slotRenderer.material = slotMaterial;
            slotRenderer.material.color = highlightColor;

            Debug.Log("Objeto colocado correctamente: " + obj.name);

            Destroy(obj);
        }
        else
        {
            Debug.LogWarning("Este slot ya está lleno o el objeto no fue aceptado.");
        }
    }
}
