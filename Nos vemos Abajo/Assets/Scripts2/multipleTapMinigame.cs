using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class multipleTapMinigame : minigame
{
    [SerializeField] int numberTaps;
    private int currentTaps;
    [SerializeField] protected InteractEvent onFinishTap;
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
            eventManager.Instance.startEvent(onFinishTap, () => { });
        }
    }
}
