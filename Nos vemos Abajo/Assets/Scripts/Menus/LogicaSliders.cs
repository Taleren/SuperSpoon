using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class LogicaSliders : MonoBehaviour
{
    public Slider sliderVolumen;

    public Slider sliderSensibilidad;

    private float valorSonido;
    private float valorSensibilidad;

    public AudioMixer audioMixer;

    public CinemachineVirtualCamera camJugador;

    void Start()
    {
        if (PlayerPrefs.HasKey("volumenAudio"))
        {
            LoadValor();
        }
        else 
        {
            ChangeSensibilidadSlider();
            ChangeVolumenSlider();
        }
    }

    public void ChangeVolumenSlider()
    {
        valorSonido = sliderVolumen.value;
        audioMixer.SetFloat("volumen", Mathf.Log10(valorSonido) * 20);
        PlayerPrefs.SetFloat("volumenAudio", valorSonido);
    }

    public void ChangeSensibilidadSlider()
    {
        valorSensibilidad = sliderSensibilidad.value;
        gameManager.Instance.savedCameraSpeedX = valorSensibilidad;
        gameManager.Instance.savedCameraSpeedY = valorSensibilidad;
        PlayerPrefs.SetFloat("sensibilidad", valorSensibilidad);
    }

    private void LoadValor() 
    {
        sliderVolumen.value = PlayerPrefs.GetFloat("volumenAudio");
        sliderSensibilidad.value = PlayerPrefs.GetFloat("sensibilidad");
        ChangeVolumenSlider();
        ChangeSensibilidadSlider();
    }
}
