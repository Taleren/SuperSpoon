using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallminigameObject : basicObject
{
    [SerializeField] protected minigame _Minigame;
    public override void Interact()
    {
        eventManager.Instance.startEvent(intEvent, () => { miniGameManager.Instance.LoadMinigame(_Minigame); });
        LeaveHover();
 
    }
}
