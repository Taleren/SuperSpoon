using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public static gameManager Instance;
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
                setCursor(true);
                break;
            case GameState.FreePlay:
                setCursor(false);

                break;
            case GameState.Minigame:
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
