using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallminigameObject : basicObject
{
    [SerializeField] protected minigame _Minigame;
    [SerializeField] protected ObjState targetState =ObjState.Active;

    public override void Interact()
    {

        if (currentState == targetState)
        {
            eventManager.Instance.startEvent(intEvent, () => { miniGameManager.Instance.LoadMinigame(_Minigame); });
        }
        else
        {
            eventManager.Instance.startEvent(intEvent, () => {  });

        }
        LeaveHover();
 
    }
}
