using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class minigameObject : basicObject
{
  [SerializeField] private  ObjState startState;
   minigame _fatherMinigame;
  [SerializeField]  List<eventData> eventList;
    // Start is called before the first frame update
    void Start()
    {
        _fatherMinigame = GetComponentInParent<minigame>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void Interact()
    {
        eventData curEvent = getState();
        eventManager.Instance.startEvent(curEvent.interactEvent, () => { curEvent.minigameEvent.Invoke(); });
    }
  private eventData getState()
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
            string txt = getState().HoverText;
            if (string.IsNullOrEmpty(txt))
            {
                txt = Hoverkey;
            }

            UIManager.Instance.showObjectName(txt);
        }
       
    }
    public void startMinigame()
    {
        currentState = startState;
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
