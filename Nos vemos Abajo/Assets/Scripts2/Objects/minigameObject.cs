using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class minigameObject : basicObject
{
   minigame _fatherMinigame;
  [SerializeField]  List<eventData> eventList;
    [SerializeField] bool resetState = true;
    // Start is called before the first frame update
public   override void Start()
    {
        base.Start();
        _fatherMinigame = GetComponentInParent<minigame>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void Interact()
    {
        eventData curEvent = getStateEvents();
        eventManager.Instance.startEvent(curEvent.interactEvent, () => { curEvent.minigameEvent.Invoke(); });
    }
  private eventData getStateEvents()
    {
        foreach (eventData _event in eventList)
        {
            if (_event.state==currentState)
            {
                return _event;
            }
        }
        print("State event is null " + currentState);
    return null;

    }
    public override void EnterHover()
    {
        if (showName)
        {
            string txt = getStateEvents().HoverText;
            if (string.IsNullOrEmpty(txt))
            {
                txt = Hoverkey;
            }

            UIManager.Instance.showObjectName(txt);
        }
       
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
[Serializable]
public class eventData
{
    public string HoverText;
    public minigameObject.ObjState state;
   public InteractEvent interactEvent;
   public UnityEvent minigameEvent;
}
