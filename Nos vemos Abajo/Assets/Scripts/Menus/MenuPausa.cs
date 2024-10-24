using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class MenuPausa : MonoBehaviour
{
    public GameObject optionsPausa;

    public GameObject menuPausa;

    public GameObject botonesPausa;

    private void Start()
    {
        menuPausa.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && gameManager.Instance.currentState == gameManager.GameState.FreePlay) 
        {
            menuPausa.SetActive(true);
            Time.timeScale = 0f;
            gameManager.Instance.setState(gameManager.GameState.Paused);
        }
    }

    public void Continuar()
    {
        menuPausa.SetActive(false);
        Time.timeScale = 1f;
        gameManager.Instance.setState(gameManager.GameState.FreePlay);
        Cursor.visible = false;
        
    }
    public void OptionsPausa()
    {
        botonesPausa.SetActive(false);
        optionsPausa.SetActive(true);

    }
    public void OptionsVolverPausa()
    {
        botonesPausa.SetActive(true);
        optionsPausa.SetActive(false);
    }

    public void SalirJuegoPausa()
    {
        Application.Quit();
        print("Saliste del videojuego");
    }
}
