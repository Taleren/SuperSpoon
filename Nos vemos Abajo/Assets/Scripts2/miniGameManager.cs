using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class miniGameManager : MonoBehaviour
{
    public static miniGameManager Instance;
    minigame currentMinigame;
    [SerializeField] Light farolLight;
    [SerializeField] float intensityH,intensityL;

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
    public void LoadMinigame(minigame _minigame)
    {
        gameManager.Instance.setState(gameManager.GameState.Minigame);
        currentMinigame = _minigame;
        _minigame.StartMinigame();
        if(farolLight.gameObject != null)
        {
            farolLight.intensity = intensityL;
        }

    }
    public void endMinigame(InteractEvent postEvent)
    {
        print("end");
        gameManager.Instance.setState(gameManager.GameState.FreePlay);
        eventManager.Instance.startEvent(postEvent,()=> { gameManager.Instance.setState(gameManager.GameState.FreePlay); });
        if (farolLight.gameObject != null)
        {
            farolLight.intensity = intensityH;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
