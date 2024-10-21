using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class minigameObject : basicObject
{
   minigame _fatherMinigame;
    [SerializeField] bool resetState = true;
    // Start is called before the first frame update
public   override void Start()
    {
        base.Start();
        _fatherMinigame = GetComponentInParent<minigame>();
    }

    // Update is called once per frame
   
    public override void Interact()
    {
        eventData curEvent = getStateEvents();
        eventManager.Instance.startEvent(curEvent.interactEvent, () => { curEvent.minigameEvent.Invoke(); });
        LeaveHover();
    }
  
    public override void EnterHover()
    {
        base.EnterHover();
       
    }
    public void startMinigame()
    {
        if (resetState)
        {

            currentState = startState;
        }
    
    }
  
  
    public void nextCall()
    {
        eventManager.Instance.playCall();
    }
}

