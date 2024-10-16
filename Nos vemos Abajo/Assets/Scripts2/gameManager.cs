using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public static gameManager Instance;
  [SerializeField]  CinemachineVirtualCamera mainCamera;
    [SerializeField] float savedCameraSpeedX;
    [SerializeField] float savedCameraSpeedY;

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
        savedCameraSpeedX = mainCamera.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed;
        savedCameraSpeedY = mainCamera.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_MaxSpeed;

        setState(GameState.FreePlay);
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

                mainCamera.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed = 0;
                mainCamera.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_MaxSpeed =0; 
                setCursor(true);
                break;
            case GameState.FreePlay:
                mainCamera.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed = savedCameraSpeedX;
                mainCamera.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_MaxSpeed = savedCameraSpeedY;


                setCursor(false);

                break;
            case GameState.Minigame:

                mainCamera.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed = 0;
                mainCamera.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_MaxSpeed = 0;
                setCursor(true);

                break;
            default:
                break;
        }
    }
    public enum GameState
    {
        Paused,
        FreePlay,
        Minigame

    }
}
