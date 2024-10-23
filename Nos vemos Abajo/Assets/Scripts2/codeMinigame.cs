using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class codeMinigame : minigame
{
 [SerializeField]   int[] correctCode;
  [SerializeField]  int[] currentCode;
   //[SerializeField] minigameObject LastObject;
    [SerializeField] int MaxVal;
   [SerializeField] TMP_Text[] texts;
    [SerializeField] protected InteractEvent onCodeEvent;


    public bool Check()
    {
        for (int i = 0; i < correctCode.Length; i++)
        {
            if(correctCode[i] != currentCode[i])
            {
                return false;
            }
        }
        return true;
    }
    public void changeCode(int i)
    {
        currentCode[i]= (currentCode[i] + 1) % MaxVal;
        texts[i].text =currentCode[i].ToString();
        if (Check())
        {
            eventManager.Instance.startEvent(onCodeEvent, () => { gameManager.Instance.setState(gameManager.GameState.FreePlay); });
        }
    }
    public override void StartMinigame()
    {
        base.StartMinigame();
        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].text = currentCode[i].ToString();
        }
    }
}
