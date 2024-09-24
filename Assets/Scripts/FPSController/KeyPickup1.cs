using UnityEngine;

public class Key1Pickup : MonoBehaviour
{
    private bool hasKey1 = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // El jugador recoge la llave 1
            hasKey1 = true;
            Debug.Log("Llave 1 recogida");

            // Desactivo la llave para que desaparezca visualmente
            gameObject.SetActive(false);
        }
    }

    // Este método me permite saber si el jugador tiene la llave 1
    public bool HasKey1()
    {
        return hasKey1;
    }
}
