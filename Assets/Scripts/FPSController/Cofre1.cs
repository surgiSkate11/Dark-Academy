using UnityEngine;

public class Chest1 : MonoBehaviour
{
    public GameObject ImageA; // Asigna la ImageA de la letra A desde el Inspector
    public GameObject key; // Asigna el GameObject de la llave desde el Inspector
    public Collider chestCollider; // Asigna el collider del cofre desde el Inspector

    private bool cofreAbierto = false; // Control para evitar que se abra múltiples veces

    private void Start()
    {
        ImageA.SetActive(false); // Asegúrate de que la ImageA esté oculta al inicio
        if (key != null)
        {
            key.SetActive(true); // Asegúrate de que la llave esté visible al inicio
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que entra en el cofre es la llave (por su tag o nombre)
        if (other.gameObject == key && !cofreAbierto)
        {
            SoltarLlave(); // Solo si la llave ha sido colocada dentro del cofre
        }
    }

    public void SoltarLlave()
    {
        if (!cofreAbierto)
        {
            cofreAbierto = true; // Marca el cofre como abierto para evitar múltiples activaciones

            // Desactivar la llave
            if (key != null)
            {
                key.SetActive(false); // La llave desaparece
            }

            // Desactivar el cofre
            gameObject.SetActive(false); // El cofre desaparece

            // Mostrar la imagen
            ImageA.SetActive(true); // Aparece la Imagen A

            Debug.Log("El cofre y la llave han desaparecido. La Imagen A ha sido revelada.");
        }
    }
}
