using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class basicObject : MonoBehaviour,IObject
{
  [SerializeField] protected InteractEvent intEvent;
  [SerializeField] private GameObject outline;
    public string Hoverkey;
    public bool showName = true;
    MaterialPropertyBlock materialPropertyBlock;
    [SerializeField] protected ObjState startState;

    [SerializeField]  protected ObjState currentState;
    [SerializeField] protected List<eventData> eventList;

    public virtual void EnterHover()

    {
        string txt = null;
        if (showName)
        {
            eventData curEvent = getStateEvents();
            if (curEvent != null)
                txt = getStateEvents().HoverText;
            if (string.IsNullOrEmpty(txt))
            {
                txt = Hoverkey;
            }

            UIManager.Instance.showObjectName(txt);
        }
       
        //materialPropertyBlock = new MaterialPropertyBlock();
        //materialPropertyBlock.SetColor("_Color", Color.white);
        //materialPropertyBlock.SetFloat("_isOutlined", 1.0f);
        //this.GetComponent<MeshRenderer>().SetPropertyBlock(materialPropertyBlock, 1);
        if (outline != null)
        {
            outline.SetActive(true);
        }
    }

    public virtual void Interact()
    {
       
        eventData curEvent = getStateEvents();
        if(curEvent == null)
        {
            curEvent = new eventData();
            curEvent.interactEvent = intEvent;
        }
        
        eventManager.Instance.startEvent(curEvent.interactEvent, () => {
  gameManager.Instance.setState(gameManager.GameState.FreePlay);        });
    }

    public virtual void LeaveHover()
    {
        UIManager.Instance.hideObjectName();
        /*
        materialPropertyBlock.SetFloat("_isOutlined", 0.0f);
        this.GetComponent<MeshRenderer>().SetPropertyBlock(materialPropertyBlock, 1);
        */
        if (outline != null)
        {
            outline.SetActive(false);
        }
    }
    public enum ObjState
    {
        Off,
        Locked,
        Active
    }
    public void setState(ObjState _state)
    {
     //   print(name);
        currentState = _state;
    }
    public void StartGame()
    {
        currentState = startState;
        //print(name+currentState);

    }

    public  virtual void Start()
    {
        gameManager.Instance.subscribeGameStart(this.StartGame);
        if (outline != null)
        {
            outline.SetActive(false);
        }
    }
    public ObjState getState() => currentState;
    protected eventData getStateEvents()
    {
        foreach (eventData _event in eventList)
        {
            if (_event.state == currentState)
            {
                return _event;
            }
        }
        print("State event is null " + currentState);
        return null;

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