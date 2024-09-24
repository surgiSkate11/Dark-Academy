using UnityEngine;

public class Chest2 : MonoBehaviour
{
    public GameObject key2Pickup; // Aqu� arrastro el objeto Key2 desde el editor
    public char characterReward = 'B'; // El car�cter que voy a mostrar cuando el cofre se abra

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Verifico si el jugador tiene la llave 2 usando el script Key2Pickup
            Key2Pickup key2Script = key2Pickup.GetComponent<Key2Pickup>();

            if (key2Script != null && key2Script.HasKey2())
            {
                // Si el jugador tiene la llave 2, abro el cofre y muestro el car�cter
                Debug.Log("Cofre 2 abierto. Car�cter obtenido: " + characterReward);

                // Desactivo el cofre para simular que se abri�
                gameObject.SetActive(false);
            }
            else
            {
                // Si el jugador no tiene la llave, no puede abrir el cofre
                Debug.Log("No tienes la llave 2 para abrir este cofre.");
            }
        }
    }
}
