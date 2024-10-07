using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class agarrarObjetos : MonoBehaviour
{
    public GameObject handPoint; // El punto en la mano donde el objeto se "pega"
    public float throwForce = 10f; // Fuerza del lanzamiento

    private GameObject pickedObject = null;

    void Update()
    {
        if (pickedObject != null)
        {
            // Soltar el objeto con "R"
            if (Input.GetKey("r"))
            {
                ReleaseObject();
            }

            // Lanzar el objeto con "F"
            if (Input.GetKeyDown(KeyCode.F))
            {
                ThrowObject();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // Detecta si el objeto tiene la etiqueta "Objeto" y no hay ningún objeto recogido
        if (other.gameObject.CompareTag("Objeto") && pickedObject == null)
        {
            // Recoger el objeto con "E"
            if (Input.GetKey("e"))
            {
                PickUpObject(other.gameObject);
            }
        }
    }

    private void PickUpObject(GameObject obj)
    {
        // Desactivar gravedad y permitir que se "pegue" a la mano
        obj.GetComponent<Rigidbody>().useGravity = false;
        obj.GetComponent<Rigidbody>().isKinematic = true;

        // Mover el objeto al punto de la mano y asignarlo como hijo del handPoint
        obj.transform.position = handPoint.transform.position;
        obj.transform.SetParent(handPoint.transform);

        // Configurar el objeto como recogido
        obj.GetComponent<Collider>().isTrigger = true;
        pickedObject = obj;
    }

    private void ReleaseObject()
    {
        // Restaurar las propiedades físicas
        pickedObject.GetComponent<Rigidbody>().useGravity = true;
        pickedObject.GetComponent<Rigidbody>().isKinematic = false;

        // Separar el objeto de la mano
        pickedObject.transform.SetParent(null);
        pickedObject.GetComponent<Collider>().isTrigger = false;

        // Eliminar la referencia al objeto
        pickedObject = null;
    }

    private void ThrowObject()
    {
        // Restaurar las propiedades físicas del objeto
        pickedObject.GetComponent<Rigidbody>().useGravity = true;
        pickedObject.GetComponent<Rigidbody>().isKinematic = false;

        // Separar el objeto de la mano
        pickedObject.transform.SetParent(null);
        pickedObject.GetComponent<Collider>().isTrigger = false;

        // Aplicar una fuerza en la dirección que está mirando el jugador
        Rigidbody rb = pickedObject.GetComponent<Rigidbody>();
        rb.AddForce(handPoint.transform.forward * throwForce, ForceMode.Impulse);

        // Eliminar la referencia al objeto lanzado
        pickedObject = null;
    }
}
