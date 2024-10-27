using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    public GameObject optionsPausa;

    public GameObject menuPausa;

    public GameObject botonesPausa;

    private gameManager.GameState gameStateAnterior;

    public GameObject transicionPanel;

    private void Start()
    {
        menuPausa.SetActive(false);

    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    if (gameManager.Instance.currentState != gameManager.GameState.Paused)
        //    {
        //        gameStateAnterior = gameManager.Instance.currentState;
        //        menuPausa.SetActive(true);
        //        Time.timeScale = 0f;
        //        gameManager.Instance.setState(gameManager.GameState.Paused);
        //    }
        //}
    }

    public void Continuar()
    {
        menuPausa.SetActive(false);
        Time.timeScale = 1f;
        gameManager.Instance.setState(gameStateAnterior);
        if (gameStateAnterior == gameManager.GameState.FreePlay) 
        {
            Cursor.visible = false;
        }
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
        StartCoroutine("volverMenuCR");
    }

    IEnumerator volverMenuCR()
    {
        menuPausa.SetActive(false);
        Time.timeScale = 1f;
        transicionPanel.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("escenaTransicion");
    }
}
