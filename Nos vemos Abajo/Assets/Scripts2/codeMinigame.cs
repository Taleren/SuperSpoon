using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class codeMinigame : minigame
{
 [SerializeField]   int[] correctCode;
  [SerializeField]  int[] currentCode;
   [SerializeField] minigameObject LastObject;
    [SerializeField] int MaxVal;
   [SerializeField] TMP_Text[] texts;

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
            LastObject.setState(basicObject.ObjState.Active);
        }
    }
    public override void StartMinigame()
    {
        base.StartMinigame();
        LastObject.setState(basicObject.ObjState.Locked);
        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].text = currentCode[i].ToString();
        }
    }
}
