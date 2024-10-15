using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class minigameObject : basicObject
{
  [SerializeField] private  miniGameObjState startState;
    private miniGameObjState currentState;
    [SerializeField] minigame _fatherMinigame;
  public  UnityEvent Mevent;
  [SerializeField]  List<eventData> eventList;
    // Start is called before the first frame update
    void Start()
    {
        
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
    return null;

    }
    public void startMinigame()
    {
        currentState = startState;
    }
    public enum miniGameObjState
    {
        Off,
        Locked,
        Active
    }
    public void setState(miniGameObjState _state)
    {
        currentState = _state;
    }
}
[Serializable]
public class eventData
{
   public minigameObject.miniGameObjState state;
   public InteractEvent interactEvent;
   public UnityEvent minigameEvent;
}
