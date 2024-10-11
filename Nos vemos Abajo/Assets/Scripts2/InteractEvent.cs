using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObject/Event")]
public class InteractEvent : ScriptableObject
{

    public  eventCalls[] eventOrder;
    public enum eventCallTypes
    {
        animCall,
        timelineCall,
        dialogueCall,
    }
}
