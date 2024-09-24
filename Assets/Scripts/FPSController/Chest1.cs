using UnityEngine;

public class Chest1 : MonoBehaviour
{
    public GameObject key1Pickup; // Aquí arrastro el objeto Key1 desde el editor
    public char characterReward = 'A'; // El carácter que voy a mostrar cuando el cofre se abra

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Verifico si el jugador tiene la llave 1 usando el script Key1Pickup
            Key1Pickup key1Script = key1Pickup.GetComponent<Key1Pickup>();

            if (key1Script != null && key1Script.HasKey1())
            {
                // Si el jugador tiene la llave 1, abro el cofre y muestro el carácter
                Debug.Log("Cofre 1 abierto. Carácter obtenido: " + characterReward);

                // Desactivo el cofre para simular que se abrió
                gameObject.SetActive(false);
            }
            else
            {
                // Si el jugador no tiene la llave, no puede abrir el cofre
                Debug.Log("No tienes la llave 1 para abrir este cofre.");
            }
        }
    }
}
