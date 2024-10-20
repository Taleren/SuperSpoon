using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class UIMenu : MonoBehaviour
{
    public GameObject optionsPanel;

    public CinemachineVirtualCamera camMenu;

    private Camera cam;

    public GameObject menuInterfaz;

    public GameObject transicionPanel;

    private void Start()
    {
        transicionPanel.SetActive(false);
        gameManager.Instance.setState(gameManager.GameState.Paused);
    }

    public void Jugar()
    {
        menuInterfaz.SetActive(false);
        camMenu.gameObject.SetActive(false);
        Cursor.visible = false;
        StartCoroutine("transicion");
        
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

    IEnumerator transicion()
    {
        transicionPanel.SetActive(true);
        yield return new WaitForSeconds(3.5f);
        transicionPanel.SetActive(false);
        SoundManager.instance.PlaySound("gritoMujer", GameObject.Find("PRUEBASONIDO").transform.position);
        yield return new WaitForSeconds(2f);
        SoundManager.instance.PlaySound("golpeIntro", GameObject.Find("PRUEBASONIDO").transform.position);
        gameManager.Instance.setState(gameManager.GameState.FreePlay);
        yield return null;

    }
}
