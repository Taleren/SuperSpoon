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
    public void setState(GameState newstate)
    {
        currentState = newstate;
        switch (newstate)
        {
            case GameState.Paused:
                setCursor(true);

                if (mainCamera.GetCinemachineComponent<CinemachinePOV>() != null)
                {
                    mainCamera.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed = 0;
                    mainCamera.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_MaxSpeed = 0;
                }

                break;
            case GameState.FreePlay:
                if (mainCamera.GetCinemachineComponent<CinemachinePOV>() != null)
                {
                    mainCamera.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed = savedCameraSpeedX;
                    mainCamera.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_MaxSpeed = savedCameraSpeedY;
                }
                setCursor(false);

                break;
            case GameState.Minigame:
                if (mainCamera.GetCinemachineComponent<CinemachinePOV>() != null)
                {
                    mainCamera.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed = 0;
                    mainCamera.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_MaxSpeed = 0;
                }
                setCursor(true);

                break;
            default:
                break;
        }
        changeState?.Invoke();
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
        Minigame

    }
    public void startTheGame()
    {
        setState(GameState.FreePlay);

        startGame?.Invoke();
    }
    
}
