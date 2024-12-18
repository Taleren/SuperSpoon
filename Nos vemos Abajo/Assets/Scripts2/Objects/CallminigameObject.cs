using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallminigameObject : basicObject
{
    [SerializeField] protected minigame _Minigame;
    [SerializeField] protected ObjState targetState =ObjState.Active;

    public override void Interact()
    {

        eventData curEvent = getStateEvents();
        if (curEvent == null)
        {
            curEvent = new eventData();
            curEvent.interactEvent = intEvent;
        }

        if (currentState == targetState)
        {
            eventManager.Instance.startEvent(curEvent.interactEvent, () => { miniGameManager.Instance.LoadMinigame(_Minigame); });
        }
        else
        {
            eventManager.Instance.startEvent(curEvent.interactEvent, () => { gameManager.Instance.setState(gameManager.GameState.FreePlay); });

        }
        LeaveHover();
 
    }
}
