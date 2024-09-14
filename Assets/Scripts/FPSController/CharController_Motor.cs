using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotorControladorPersonaje : MonoBehaviour {

    // Velocidad de movimiento del personaje
    public float velocidad = 10.0f;

    // Sensibilidad del ratón para la rotación de la cámara
    public float sensibilidad = 30.0f;

    // Altura del agua, si el personaje cae por debajo de esta altura, se ajusta la gravedad
    public float alturaDelAgua = 15.5f;

    // Componente CharacterController para manejar el movimiento del personaje
    CharacterController controladorPersonaje;

    // Cámara del personaje
    public GameObject camara;

    // Variables para el movimiento del personaje en los ejes frontal/trasero (FB) y lateral (LR)
    float moverFB, moverLR;

    // Variables para la rotación del personaje en los ejes X e Y
    float rotacionX, rotacionY;

    // Controla si el clic derecho en WebGL controla la rotación
    public bool rotacionConClickDerechoWebGL = true;

    // Gravedad para aplicar en el movimiento vertical del personaje
    float gravedad = -9.8f;

    void Start(){
        // Bloquea el cursor (comentado, puede usarse para que el cursor no se vea)
        // LockCursor ();

        // Obtiene el componente CharacterController del personaje
        controladorPersonaje = GetComponent<CharacterController> ();

        // Si estamos en el editor de Unity, desactiva la rotación con clic derecho para WebGL
        // y aumenta la sensibilidad del ratón.
        if (Application.isEditor) {
            rotacionConClickDerechoWebGL = false;
            sensibilidad = sensibilidad * 1.5f;
        }
    }

    // Verifica si el personaje está por debajo de la altura del agua y ajusta la gravedad
    void VerificarAlturaDelAgua(){
        if (transform.position.y < alturaDelAgua) {
            // Si el personaje está bajo el agua, la gravedad se anula
            gravedad = 0f;
        } else {
            // Si el personaje está fuera del agua, la gravedad normal se aplica
            gravedad = -9.8f;
        }
    }

    void Update(){
        // Obtener el input de movimiento horizontal y vertical del teclado (WASD o flechas)
        moverFB = Input.GetAxis ("Horizontal") * velocidad;
        moverLR = Input.GetAxis ("Vertical") * velocidad;

        // Obtener la rotación del ratón en los ejes X e Y
        rotacionX = Input.GetAxis ("Mouse X") * sensibilidad;
        rotacionY = Input.GetAxis ("Mouse Y") * sensibilidad;

        // Comprobar la altura del agua para ajustar la gravedad si es necesario
        VerificarAlturaDelAgua ();

        // Crear un vector de movimiento (X = lateral, Y = gravedad, Z = frontal)
        Vector3 movimiento = new Vector3 (moverFB, gravedad, moverLR);

        // Si estamos en WebGL y usamos el clic derecho para la rotación
        if (rotacionConClickDerechoWebGL) {
            if (Input.GetKey (KeyCode.Mouse0)) {
                // Si se presiona el clic izquierdo, rotamos la cámara
                RotacionDeCamara (camara, rotacionX, rotacionY);
            }
        } else if (!rotacionConClickDerechoWebGL) {
            // Si no estamos en WebGL, rotamos la cámara directamente sin necesidad de clic
            RotacionDeCamara (camara, rotacionX, rotacionY);
        }

        // Aplicar la rotación al movimiento del personaje
        movimiento = transform.rotation * movimiento;

        // Mover al personaje usando el controlador CharacterController
        controladorPersonaje.Move (movimiento * Time.deltaTime);
    }

    // Método para rotar la cámara y el personaje
    void RotacionDeCamara(GameObject camara, float rotX, float rotY){
        // Rotar el personaje en el eje Y (horizontal)
        transform.Rotate (0, rotX * Time.deltaTime, 0);

        // Rotar la cámara en el eje X (vertical), pero sin inclinar demasiado
        camara.transform.Rotate (-rotY * Time.deltaTime, 0, 0);
    }

}
