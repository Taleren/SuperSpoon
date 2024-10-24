using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class UIMenu : MonoBehaviour
{
    public GameObject optionsPanel;

    public GameObject botonesMenu;

    public CinemachineVirtualCamera camMenu;

    private Camera cam;

    public GameObject menuInterfaz;

    public GameObject transicionPanel;

    private void Start()
    {
        SoundManager.instance.PlaySound("ventisca", transform.position);
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
        optionsPanel.SetActive(true);
        botonesMenu.SetActive(false);
        
    }
    public void OptionsPanelVolver()
    {
        optionsPanel.SetActive(false);
        botonesMenu.SetActive(true);
    }


    public void SalirJuego()
    {
        Application.Quit();
        print("Saliste del videojuego");
    }

    IEnumerator transicion()
    {
        transicionPanel.SetActive(true);
        yield return new WaitForSeconds(2f);
        SoundManager.instance.PlaySound("gritoMujer", GameObject.Find("SonidosIntroObjeto").transform.position);
        yield return new WaitForSeconds(3f);
        transicionPanel.SetActive(false);
        SoundManager.instance.PlaySound("susto", GameObject.Find("SonidosIntroObjeto").transform.position);
        yield return new WaitForSeconds(2f);
        gameManager.Instance.startTheGame();
        yield return null;

    }
}
