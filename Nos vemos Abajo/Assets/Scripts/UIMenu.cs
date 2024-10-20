using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenu : MonoBehaviour
{
    public GameObject optionsPanel;

    public void Jugar()
    {
        // Movimiento de cámara + liberación
    }
    public void OptionsPanel()
    {
        if (Time.timeScale == 1f)
        {
            Time.timeScale = 0;
            optionsPanel.SetActive(true);
        }
    }
    public void OptionsPanelVolver()
    {
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
            optionsPanel.SetActive(false);
        }
    }


    public void SalirJuego()
    {
        Application.Quit();
        print("Saliste del videojuego");
    }
}
