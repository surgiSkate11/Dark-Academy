using UnityEngine;

public class Key2Pickup : MonoBehaviour
{
    private bool hasKey2 = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // El jugador recoge la llave 2
            hasKey2 = true;
            Debug.Log("Llave 2 recogida");

            // Desactivo la llave para que desaparezca visualmente
            gameObject.SetActive(false);
        }
    }

    // Este método me permite saber si el jugador tiene la llave 2
    public bool HasKey2()
    {
        return hasKey2;
    }
}
