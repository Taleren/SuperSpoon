using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{

    public static gameManager Instance;
  [SerializeField]  CinemachineVirtualCamera mainCamera;
    public float savedCameraSpeedX;
    public float savedCameraSpeedY;
    Action changeState;
    Action startGame;
    [SerializeField] GameObject  hands;
  [SerializeField]  Canvas gameCursorCanvas;
    [SerializeField] protected InteractEvent startGameEvent;

    public GameState currentState { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        hands.SetActive(false);
        setCursor(true);
        setGameCursor(false);
        //mainCamera = (CinemachineVirtualCamera)Camera.main.GetComponent<CinemachineBrain>().ActiveVirtualCamera;

        // StartCoroutine(delayStart());
    }
    IEnumerator delayStart()
    {
        yield return new WaitForEndOfFrame();
        //startTheGame();


    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void callPingPong(string callText,Action nextAction)
    {
        print(callText);
        nextAction.Invoke();
    }
    void setCursor(bool free)
    {
        Cursor.visible = free;
        Cursor.lockState = free ? CursorLockMode.None : CursorLockMode.Locked;
    }
    void setGameCursor(bool show)
    {
        //gameCursorCanvas.gameObject.SetActive(show);
    }
    public void setState(GameState newstate)
    {
        print("setState");
        print(newstate);
        currentState = newstate;
        switch (newstate)
        {
            case GameState.Paused:

                if (mainCamera.GetCinemachineComponent<CinemachinePOV>() != null)
                {
                    setCameraSpeed(false);

                }
                setCursor(true);

                setGameCursor(false);

                break;
            case GameState.FreePlay:
                if (mainCamera.GetCinemachineComponent<CinemachinePOV>() != null)
                {
                    setCameraSpeed(true);

                }
                setCursor(false);
                setGameCursor(true);

                break;
            case GameState.Minigame:
                if (mainCamera.GetCinemachineComponent<CinemachinePOV>() != null)
                {
                    print("set speed");
                    setCameraSpeed(false);
                }
                else
                {
                    print("minigame mal");

                }
                setCursor(true);
                setGameCursor(false);

                break;
            case GameState.onInteract:
                //if (mainCamera.GetCinemachineComponent<CinemachinePOV>() != null)
                //{
                //    setCameraSpeed(true);
                //}
                setCursor(false);
                setGameCursor(false);
                break;
            default:
                break;
        }
        changeState?.Invoke();
    }
    public void setCameraSpeed(bool isOn)
    {
        mainCamera.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed = isOn ? savedCameraSpeedX : 0;
        mainCamera.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_MaxSpeed = isOn ? savedCameraSpeedY : 0;

    }
    public void setChangeStateEvent(Action action)
    {
        changeState += action;
    }
    public void subscribeGameStart(Action action)
    {
        startGame += action;
    }
    public enum GameState
    {
        Paused,
        FreePlay,
        Minigame,
        onInteract

    }
    public void startTheGame()
    {
        setState(GameState.FreePlay);
        hands.SetActive(false);

        startGame?.Invoke();

        eventManager.Instance.startEvent(startGameEvent, () => { gameManager.Instance.setState(gameManager.GameState.FreePlay); });

    }

}
