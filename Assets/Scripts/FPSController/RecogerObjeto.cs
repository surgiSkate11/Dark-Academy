using UnityEngine;

public class RecogerObjeto : MonoBehaviour
{
    public GameObject handPoint;
    public float throwForce = 10f;
    private GameObject pickedObject = null;
    private GameObject currentObject = null;
    private float pickupCooldown = 0.5f;
    private float lastPickupTime = 0f;

    // Lista de etiquetas aceptadas
    public string[] acceptedTags = { "Objeto", "ImageA", "ImageB", "ImageC", "ImageD" };

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentObject != null && pickedObject == null && Time.time > lastPickupTime + pickupCooldown)
            {
                PickUpObject(currentObject);
            }
            else if (pickedObject != null)
            {
                CheckPlaceInSlot();
            }
        }

        if (pickedObject != null && Input.GetKeyDown(KeyCode.R))
        {
            ReleaseObject();
        }

        if (pickedObject != null && Input.GetKeyDown(KeyCode.F))
        {
            ThrowObject();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Comprueba si el objeto tiene una etiqueta aceptada
        if (IsAcceptedTag(other.gameObject.tag) && pickedObject == null)
        {
            currentObject = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (IsAcceptedTag(other.gameObject.tag))
        {
            currentObject = null;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Slot") && pickedObject != null)
        {
            Slot slot = other.GetComponent<Slot>();
            if (slot != null && Input.GetKeyDown(KeyCode.E))
            {
                PlaceInSlot(slot);
            }
        }
    }

    private void PickUpObject(GameObject obj)
    {
        if (Time.time < lastPickupTime + pickupCooldown) return;

        Rigidbody rb = obj.GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = true;
        obj.transform.position = handPoint.transform.position;
        obj.transform.SetParent(handPoint.transform);
        obj.GetComponent<Collider>().isTrigger = true;

        pickedObject = obj;
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
        lastPickupTime = Time.time;
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
        lastPickupTime = Time.time;
    }

    private void CheckPlaceInSlot()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 1f);
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
        if (slot.acceptedObjectTag != pickedObject.tag)
        {
            Debug.Log($"El objeto no coincide. Se esperaba: {slot.acceptedObjectTag} pero llegó: {pickedObject.tag}");
            return;
        }

        pickedObject.transform.position = slot.transform.position;
        pickedObject.transform.SetParent(slot.transform);
        Rigidbody rb = pickedObject.GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = true;

        slot.PlaceObject(pickedObject);
        pickedObject = null;
    }

    private bool IsAcceptedTag(string tag)
    {
        foreach (string acceptedTag in acceptedTags)
        {
            if (tag == acceptedTag)
            {
                return true;
            }
        }
        return false;
    }
}
