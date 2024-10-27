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

    [SerializeField] GameObject rigidBodySound;

    [SerializeField] private InteractEvent onStart;


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
        rigidBodySound.gameObject.GetComponent<Rigidbody>().useGravity = true;

        yield return new WaitForSeconds(2f);
        eventManager.Instance.startEvent(onStart, () => { });

        SoundManager.instance.PlaySound("gritoMujer", rigidBodySound.transform.position,rigidBodySound);
        yield return new WaitForSeconds(3f);
        transicionPanel.SetActive(false);
        SoundManager.instance.PlaySound("susto", GameObject.Find("SonidosIntroObjeto").transform.position);
        yield return new WaitForSeconds(2f);
      Destroy(rigidBodySound);

        gameManager.Instance.startTheGame();
        yield return null;

    }
}
