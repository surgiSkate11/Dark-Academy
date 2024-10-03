using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Para controlar la imagen final del juego

public class SlotManager : MonoBehaviour
{
    // Referencia a los 4 slots donde se colocar�n las im�genes
    public GameObject[] slots;

    // Para controlar cu�ntos slots est�n llenos
    private int filledSlots = 0;

    // Referencia a la imagen de fin del juego
    public GameObject endGameImage;

    // Start se ejecuta al inicio
    void Start()
    {
        // Inicialmente la imagen de fin de juego debe estar desactivada
        endGameImage.SetActive(false);
    }

    // Este m�todo se llamar� cada vez que un objeto entre en un slot
    public void AddObjectToSlot()
    {
        filledSlots++;

        // Si los 4 slots est�n llenos, mostramos la imagen del final del juego
        if (filledSlots >= 4)
        {
            ShowEndGameImage();
        }
    }

    // Este m�todo se llamar� cada vez que se retire un objeto de un slot
    public void RemoveObjectFromSlot()
    {
        filledSlots--;
    }

    // Mostrar la imagen del final del juego
    void ShowEndGameImage()
    {
        // Activa la imagen de fin de juego
        endGameImage.SetActive(true);
    }
}
