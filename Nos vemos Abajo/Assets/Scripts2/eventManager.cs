using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eventManager : MonoBehaviour
{
    eventCalls[] currentCalls;
    int callIndex;
    Action currentNextAction;

    public static eventManager Instance;
    // Start is called before the first frame update
    void Start()
    {
        
    }
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
    // Update is called once per frame
    void Update()
    {
        
    }
    public void startEvent(InteractEvent _event,Action nextAction)
    {
        currentCalls = _event.eventOrder;
        currentNextAction = nextAction;
        callIndex = 0;
        playCall();
    }

    public void playCall()
    {
        if (callIndex < currentCalls.Length)
        {
            eventCalls Call = currentCalls[callIndex];
            switch (Call.callType)
            {
                case InteractEvent.eventCallTypes.animCall:
                    gameManager.Instance.callPingPong("Playing animation: ", () => { print("siguiente call"); callIndex++; playCall(); });
                    break;
                case InteractEvent.eventCallTypes.timelineCall:
                    gameManager.Instance.callPingPong("Playing timeline: " + Call.timelineObj, () => { print("siguiente call"); callIndex++; playCall(); });

                    break;
                case InteractEvent.eventCallTypes.dialogueCall:
                    gameManager.Instance.callPingPong("Playing Dialogue: " + Call.nameKey, () => { print("siguiente call"); callIndex++; playCall(); });


                    break;
                
                default:
                    break;
            }
        }
        else
        {
            currentNextAction.Invoke();
        }
    }

   
}
