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
        gameManager.Instance.setState(gameManager.GameState.onInteract);
        
            currentCalls = _event.eventOrder;
            currentNextAction = nextAction;
            callIndex = -1;
            playCall();
        
    }

    public void playCall()
    {

        callIndex++;

       // print("Play call");
        print(currentCalls.Length+ " "+callIndex);
        if (callIndex < currentCalls.Length)
        {
            eventCalls Call = currentCalls[callIndex];
          print(Call.callType);
            Action act = () => { };
            if (!Call.PlayWithBefore)
            {
                act = () => { playCall(); };
            }
            switch (Call.callType)
            {
                case InteractEvent.eventCallTypes.animCall:
                    Call.animator.Play(Call.nameKey, -1, 0.0f);

                    break;
                case InteractEvent.eventCallTypes.timelineCall:
                    timelineManager.Instance.callTimeline(Call.timelineObj, act);
                    break;
                case InteractEvent.eventCallTypes.dialogueCall:
                    TextManager.Instance.playDialogue(Call.nameKey, act);
                    // gameManager.Instance.callPingPong("Playing Dialogue: " + Call.nameKey, () => { print("siguiente call"); playCall(); });


                    break;
                case InteractEvent.eventCallTypes.changeObjectState:
                    Call.interactObject.setState(Call.newState);
                    if (!Call.PlayWithBefore)
                    {
                        playCall();
                    }

                    break;
                case InteractEvent.eventCallTypes.soundState:
                    SoundManager.instance.PlaySound(Call.nameKey, Call.Transform.position, Call.obj);
                    if (!Call.PlayWithBefore)
                    {
                        playCall();
                    }
                    break;
                case InteractEvent.eventCallTypes.activateObject:
                    Call.obj.SetActive(Call.Boolean);
                    if (!Call.PlayWithBefore)
                    {
                        playCall();
                    }
                    break;
                case InteractEvent.eventCallTypes.waitState:
                    StartCoroutine(waitTime(Call.waitTime));
                    break;
                case InteractEvent.eventCallTypes.changeOptionals:
                    
                        gameManager.Instance.lockOptionals(Call.newState);
                    
                  
                    if (!Call.PlayWithBefore)
                    {
                        playCall();
                    }

                    break;
                default:
                    break;
            }
            if (callIndex+1 < currentCalls.Length && currentCalls[callIndex+1].PlayWithBefore)
            {
                playCall();
            }
        }
        else
        {
            print("currentaction Invoke");
            currentNextAction.Invoke();
        }
    }
    IEnumerator waitTime(float time)
    {
        yield return new WaitForSeconds(time);
        playCall();
    }
   
}
