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
        callIndex = -1;
        playCall();
    }

    public void playCall()
    {
        callIndex++;
        print("Play call");
        print(currentCalls.Length);
        if (callIndex < currentCalls.Length)
        {
            eventCalls Call = currentCalls[callIndex];
            switch (Call.callType)
            {
                case InteractEvent.eventCallTypes.animCall:
                    Call.animator.Play(Call.nameKey,-1, 0.0f);
                    break;
                case InteractEvent.eventCallTypes.timelineCall:
                    timelineManager.Instance.callTimeline(Call.timelineObj, () => { print("siguiente call"); playCall(); });
                    break;
                case InteractEvent.eventCallTypes.dialogueCall:
                    gameManager.Instance.callPingPong("Playing Dialogue: " + Call.nameKey, () => { print("siguiente call"); playCall(); });


                    break;
                case InteractEvent.eventCallTypes.changeObjectState:
                    Call.interactObject.setState(Call.newState);
                    break;
                default:
                    break;
            }
        }
        else
        {
            print("currentaction Invoke");
            currentNextAction.Invoke();
        }
    }

   
}
