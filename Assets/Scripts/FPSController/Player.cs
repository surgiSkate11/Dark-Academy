using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 50f; // Velocidad de movimiento normal
    public float crouchSpeed = 3.5f; // Velocidad de movimiento agachado
    public float jumpForce = 15f; // Fuerza de salto
    public float gravityMultiplier = 2.0f; // Multiplicador de gravedad
    public float rotationSpeed = 250f; // Suavizar la velocidad de rotación
    public float runSpeed = 7f; // Velocidad al caminar/correr

    public Camera playerCamera; // Cámara del jugador

    // Ajustar la sensibilidad del mouse
    public float lookSpeedX = 1000f; // Velocidad de mirada horizontal
    public float lookSpeedY = 1000f; // Velocidad de mirada vertical
    public float upperLookLimit = 90f; // Límite superior de la mirada
    public float lowerLookLimit = -90f; // Límite inferior de la mirada

    private Rigidbody rb; // Referencia al Rigidbody del jugador
    private float x, y; // Ejes de entrada del movimiento
    private bool isGrounded; // Controla si el jugador está en el suelo
    private bool isCrouching = false; // Controla si el jugador está agachado

    private float rotationY = 0; // Almacena la rotación en Y para la cámara
    private float currentSpeed; // Velocidad actual
    private float cameraVerticalRotation = 0; // Rotación vertical de la cámara

    // Añadir Animator
    public Animator animator; // Referencia al componente Animator

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Posicionar la cámara en la parte superior de la cápsula del jugador
        if (playerCamera != null)
        {
            playerCamera.transform.position = transform.position + new Vector3(0, 2.5f, 0); // Ajustar altura de la cámara
            playerCamera.transform.rotation = transform.rotation; // Asegurar que la cámara esté orientada correctamente
        }

        // Deshabilitar el cursor y establecer la rotación inicial
        Cursor.visible = false;
        rotationY = transform.eulerAngles.y; // Inicializar la rotación Y
        currentSpeed = moveSpeed; // Inicializar la velocidad de movimiento
    }

    private void Update()
    {
        // Verificar si el jugador está en el suelo usando Raycast
        CheckGroundStatus();

        // Obtener el input del jugador (ejes de movimiento)
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        // Actualizar Animator con los valores de movimiento
        animator.SetFloat("VelX", x);
        animator.SetFloat("VelY", y);

        // Verificar si el jugador está agachado
        if (Input.GetKey(KeyCode.C)) // Cambia a "C" para agacharse
        {
            isCrouching = true;
            currentSpeed = crouchSpeed; // Ajustar velocidad al agacharse
            transform.localScale = new Vector3(1, 0.5f, 1); // Cambiar la escala para simular agachado
            animator.SetBool("isCrouching", true); // Activar animación de agachado
        }
        else
        {
            isCrouching = false;
            currentSpeed = moveSpeed; // Usar la velocidad de movimiento normal
            transform.localScale = new Vector3(1, 1, 1); // Restablecer la escala cuando no está agachado
            animator.SetBool("isCrouching", false); // Desactivar animación de agachado
        }

        // Saltar solo si se presiona la tecla "Space" y el jugador está en el suelo
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
            animator.SetTrigger("Jump"); // Activar animación de salto
        }

        // Control de la mirada del jugador
        LookAround();

        // Control de la animación de correr basado en la velocidad de movimiento
        float moveMagnitude = new Vector2(x, y).magnitude;
        animator.SetFloat("Speed", moveMagnitude);
    }

    private void FixedUpdate()
    {
        // Aplicar movimiento y rotación
        MovePlayer();

        // Aplica gravedad adicional para que caiga más rápido
        if (!isGrounded)
        {
            rb.AddForce(Physics.gravity * gravityMultiplier, ForceMode.Acceleration);
        }
    }

    private void MovePlayer()
    {
        // Calcular la dirección de movimiento basada en la dirección en la que está mirando la cámara
        Vector3 moveDirection = playerCamera.transform.forward * y + playerCamera.transform.right * x;
        moveDirection.y = 0; // Evitar movimiento vertical

        // Rotar el jugador usando la entrada de rotación
        transform.Rotate(0, x * Time.deltaTime * rotationSpeed, 0);

        // Usar el Rigidbody para moverse solo si hay input
        if (moveDirection.magnitude > 0)
        {
            rb.MovePosition(rb.position + moveDirection.normalized * currentSpeed * Time.fixedDeltaTime);
        }
    }

    private void Jump()
    {
        // Reiniciar la velocidad vertical antes de aplicar la fuerza de salto
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded = false; // Asumir que está en el aire hasta que vuelva a tocar el suelo
    }

    private void CheckGroundStatus()
    {
        // Usar un raycast para verificar si el jugador está tocando el suelo
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }

    private void OnCollisionStay(Collision collision)
    {
        // Si está tocando el suelo o una plataforma, establecer isGrounded en verdadero
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Platform"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // Si deja de tocar el suelo o la plataforma, está en el aire
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Platform"))
        {
            isGrounded = false;
        }
    }

    private void LookAround()
    {
        // Obtener la entrada del mouse (sin usar Time.deltaTime)
        float mouseX = Input.GetAxis("Mouse X") * lookSpeedX;
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeedY;

        // Rotar el jugador horizontalmente (Eje Y)
        rotationY += mouseX; // Sumar la entrada del mouse a la rotación Y

        // Aplicar directamente la rotación sin suavizado
        transform.rotation = Quaternion.Euler(0, rotationY, 0);

        // Rotar la cámara verticalmente (Eje X)
        cameraVerticalRotation -= mouseY;  // Invertir rotación vertical para que sea natural
        cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, lowerLookLimit, upperLookLimit);  // Limitar la rotación vertical

        // Aplicar la rotación vertical a la cámara
        playerCamera.transform.localRotation = Quaternion.Euler(cameraVerticalRotation, 0, 0);
    }
}
