using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainPanel : MonoBehaviour
{
    [Header("Options")]
    public Slider volumeFX;
    public Slider volumeMaster;
    public Toggle mute;
    public AudioMixer mixer;
    public AudioSource fxSource;
    public AudioClip clickSound;
    private float lastVolume;

    [Header("Panels")]
    public GameObject mainPanel;
    public GameObject optionsPanel;
    public GameObject controlsPanel; // Panel de Controles

    private void Awake()
    {
        volumeFX.onValueChanged.AddListener(ChangeVolumeFX);
        volumeMaster.onValueChanged.AddListener(ChangeVolumeMaster);
    }

    // Función que carga la escena "Intro"
    public void PlayGame()
    {
        SceneManager.LoadScene("MainStory"); // Carga la escena llamada "MainStory"
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void SetMute()
    {
        if (mute.isOn)
        {
            mixer.GetFloat("VolMaster", out lastVolume);
            mixer.SetFloat("VolMaster", -80);
        }
        else
        {
            mixer.SetFloat("VolMaster", lastVolume);
        }
    }

    // Método para abrir el panel de opciones
    public void OpenOptionsPanel()
    {
        OpenPanel(optionsPanel);
    }

    // Método para abrir el panel de controles
    public void OpenControlsPanel()
    {
        OpenPanel(controlsPanel);
    }

    // Método general para abrir un panel específico
    public void OpenPanel(GameObject panel)
    {
        // Desactivar todos los paneles primero
        mainPanel.SetActive(false);
        optionsPanel.SetActive(false);
        controlsPanel.SetActive(false); // Asegúrate de que el panel de controles esté desactivado

        // Activar el panel solicitado
        panel.SetActive(true);
        PlaySoundButton();
    }

    public void ChangeVolumeMaster(float v)
    {
        mixer.SetFloat("VolMaster", v);
    }

    public void ChangeVolumeFX(float v)
    {
        mixer.SetFloat("VolFX", v);
    }

    public void PlaySoundButton()
    {
        fxSource.PlayOneShot(clickSound);
    }
}
