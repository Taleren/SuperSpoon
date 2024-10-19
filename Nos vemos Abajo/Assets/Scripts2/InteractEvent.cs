using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//[CreateAssetMenu(menuName = "ScriptableObject/Event")]

[Serializable]
public class InteractEvent 
{

    public  eventCalls[] eventOrder;
    public enum eventCallTypes
    {
        animCall,
        timelineCall,
        dialogueCall,
        changeObjectState
    }
}
