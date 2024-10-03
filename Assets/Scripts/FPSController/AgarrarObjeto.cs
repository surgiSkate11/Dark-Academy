using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgarrarObjeto : MonoBehaviour
{
    public GameObject handPoint; // El punto donde el jugador sostiene el objeto
    private GameObject pickedObject = null;

    void Update()
    {
        // Soltar el objeto con la tecla "r"
        if (pickedObject != null && Input.GetKeyDown("r"))
        {
            SoltarObjeto();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // Recoger el objeto si el jugador presiona "e" y no tiene objeto agarrado
        if (other.gameObject.CompareTag("Imagen") && Input.GetKeyDown("e") && pickedObject == null)
        {
            RecogerObjeto(other.gameObject);
        }
    }

    void RecogerObjeto(GameObject objeto)
    {
        // Desactivar la física mientras el objeto está en la mano
        Rigidbody objetoRb = objeto.GetComponent<Rigidbody>();
        if (objetoRb != null)
        {
            objetoRb.useGravity = false;
            objetoRb.isKinematic = true;
        }

        // Posicionar el objeto en la mano
        objeto.transform.position = handPoint.transform.position;
        objeto.transform.rotation = handPoint.transform.rotation;
        objeto.transform.SetParent(handPoint.transform);

        // Activar el trigger para que no interfiera con colisiones
        Collider objetoCollider = objeto.GetComponent<Collider>();
        if (objetoCollider != null)
        {
            objetoCollider.isTrigger = true;
        }

        pickedObject = objeto; // Guardar referencia al objeto recogido
    }

    void SoltarObjeto()
    {
        // Restaurar la física del objeto al soltarlo
        Rigidbody objetoRb = pickedObject.GetComponent<Rigidbody>();
        if (objetoRb != null)
        {
            objetoRb.useGravity = true;
            objetoRb.isKinematic = false;
        }

        // Separar el objeto de la mano
        pickedObject.transform.SetParent(null);

        // Desactivar el trigger para que vuelva a interactuar con el mundo
        Collider objetoCollider = pickedObject.GetComponent<Collider>();
        if (objetoCollider != null)
        {
            objetoCollider.isTrigger = false;
        }

        pickedObject = null; // Limpiar referencia al objeto
    }
}
