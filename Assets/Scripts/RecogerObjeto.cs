using UnityEngine;

public class RecogerObjeto : MonoBehaviour
{
    public GameObject handPoint; // El punto en la mano donde el objeto se "pega"
    public float throwForce = 10f; // Fuerza del lanzamiento

    private GameObject pickedObject = null;
    private GameObject currentObject = null; // Objeto que el jugador está intentando recoger

    void Update()
    {
        // Comprobar si se está presionando la tecla E
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentObject != null && pickedObject == null)
            {
                PickUpObject(currentObject);
            }
            else if (pickedObject != null)
            {
                // Si el objeto ya está recogido, intentamos colocar en el slot
                CheckPlaceInSlot();
            }
        }

        // Liberar el objeto con R
        if (pickedObject != null && Input.GetKeyDown(KeyCode.R))
        {
            ReleaseObject();
        }

        // Lanzar el objeto con F
        if (pickedObject != null && Input.GetKeyDown(KeyCode.F))
        {
            ThrowObject();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Al entrar en el trigger, se asigna el objeto actual
        if (other.gameObject.CompareTag("Objeto"))
        {
            currentObject = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Al salir del trigger, se borra la referencia
        if (other.gameObject.CompareTag("Objeto"))
        {
            currentObject = null;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Slot") && pickedObject != null)
        {
            // Verificar si el objeto actual puede ser colocado en el slot
            Slot slot = other.GetComponent<Slot>();
            if (slot != null && Input.GetKeyDown(KeyCode.E))
            {
                PlaceInSlot(slot);
            }
        }
    }

    private void PickUpObject(GameObject obj)
    {
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = true;

        obj.transform.position = handPoint.transform.position;
        obj.transform.SetParent(handPoint.transform);

        obj.GetComponent<Collider>().isTrigger = true;
        pickedObject = obj;

        // Limpiar la referencia al objeto actual
        currentObject = null;
    }

    private void ReleaseObject()
    {
        if (pickedObject == null) return;

        Rigidbody rb = pickedObject.GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.isKinematic = false;

        pickedObject.transform.SetParent(null);
        pickedObject.GetComponent<Collider>().isTrigger = false;

        pickedObject = null;
    }

    private void ThrowObject()
    {
        if (pickedObject == null) return;

        Rigidbody rb = pickedObject.GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.isKinematic = false;

        pickedObject.transform.SetParent(null);
        pickedObject.GetComponent<Collider>().isTrigger = false;

        rb.AddForce(handPoint.transform.forward * throwForce, ForceMode.Impulse);

        pickedObject = null;
    }

    private void CheckPlaceInSlot()
    {
        // Comprobar si estamos en un slot para colocar el objeto
        Collider[] colliders = Physics.OverlapSphere(transform.position, 1f); // Ajustar el radio si es necesario
        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Slot"))
            {
                Slot slot = collider.GetComponent<Slot>();
                if (slot != null && slot.AcceptsObject(pickedObject))
                {
                    PlaceInSlot(slot);
                    return;
                }
            }
        }
    }

    private void PlaceInSlot(Slot slot)
    {
        pickedObject.transform.position = slot.transform.position;
        pickedObject.transform.SetParent(slot.transform);

        Rigidbody rb = pickedObject.GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = true;
        pickedObject.GetComponent<Collider>().isTrigger = true;

        slot.PlaceObject(pickedObject);
       
        // Llama a UpdateSlotColor para verificar los slots llenos
        FindObjectOfType<SlotManager>().UpdateSlotColor(slot.gameObject);

        pickedObject = null;
    }
}
