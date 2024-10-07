using UnityEngine;

public class AgarrarObjeto : MonoBehaviour
{
    private bool isDragging = false;
    private Rigidbody currentlyDraggedRigidbody;
    private Vector3 offset;
    private int originalLayer;

    public float smoothSpeed = 5f; // Velocidad de movimiento suave
    public float reachDistance = 3f; // Distancia para poder interactuar con objetos

    private Camera playerCamera;

    void Start()
    {
        playerCamera = Camera.main; // Obtiene la cámara principal
    }

    void Update()
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition); // Ray desde el mouse
        RaycastHit hit;

        // Si se presiona "E" o el mouse está presionado y no estamos arrastrando nada
        if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.E)) && !isDragging)
        {
            if (Physics.Raycast(ray, out hit, reachDistance))
            {
                Rigidbody hitRigidbody = hit.collider.GetComponent<Rigidbody>();
                if (hitRigidbody != null)
                {
                    StartDragging(hitRigidbody, hit.point);
                }
            }
        }

        // Soltar el objeto con "R" o al soltar el mouse
        if (isDragging && (Input.GetMouseButtonUp(0) || Input.GetKeyDown(KeyCode.R)))
        {
            StopDragging();
        }

        // Si estamos arrastrando, mover el objeto
        if (isDragging && currentlyDraggedRigidbody != null)
        {
            Vector3 targetPosition = playerCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, reachDistance));
            MoveWithCollisions(targetPosition);
        }
    }

    private void StartDragging(Rigidbody hitRigidbody, Vector3 hitPoint)
    {
        isDragging = true;
        currentlyDraggedRigidbody = hitRigidbody;

        originalLayer = currentlyDraggedRigidbody.gameObject.layer;
        currentlyDraggedRigidbody.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");

        offset = currentlyDraggedRigidbody.transform.position - hitPoint;
        currentlyDraggedRigidbody.isKinematic = true;
    }

    private void StopDragging()
    {
        currentlyDraggedRigidbody.gameObject.layer = originalLayer;
        isDragging = false;
        currentlyDraggedRigidbody.isKinematic = false;
        currentlyDraggedRigidbody = null;
    }

    private void MoveWithCollisions(Vector3 targetPosition)
    {
        currentlyDraggedRigidbody.MovePosition(Vector3.Lerp(currentlyDraggedRigidbody.transform.position, targetPosition, smoothSpeed * Time.deltaTime));
    }
}
