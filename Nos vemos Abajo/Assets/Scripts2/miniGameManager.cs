using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miniGameManager : MonoBehaviour
{
    public static miniGameManager Instance;
    minigame currentMinigame;
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

        _minigame.StartMinigame();
    }
    public void endMinigame()
    {
        currentMinigame.endMinigame();
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