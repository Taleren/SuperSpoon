using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class multipleTapMinigame : minigame
{
    [SerializeField] int numberTaps;
    private int currentTaps;
    [SerializeField] protected InteractEvent onFinishTap;
    [SerializeReference] bool endOnTap = false;
    public override void StartMinigame()
    {
        base.StartMinigame();
        currentTaps = 0;
    }
    public void Tap()
    {
        currentTaps++;
        if(currentTaps>= numberTaps)
        {
            if (endOnTap)
            {
                eventManager.Instance.startEvent(onFinishTap, () => { this.endMinigame(); });

            }
            else
            {
                eventManager.Instance.startEvent(onFinishTap, () => { gameManager.Instance.setState(gameManager.GameState.FreePlay); });
            }
        }
    }
}
