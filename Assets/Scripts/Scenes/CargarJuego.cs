using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement; // Para cambiar de escena

public class CargarJuego : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    // Nombre de la escena a la que se desea cambiar
    public string Game;

    void Start()
    {
        // Obtener el componente VideoPlayer
        videoPlayer = GetComponent<VideoPlayer>();

        // Escuchar el evento cuando el video termina
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    // Método que se ejecuta cuando el video termina
    void OnVideoEnd(VideoPlayer vp)
    {
        // Cambiar a la escena especificada
        SceneManager.LoadScene("Game");
    }
}


