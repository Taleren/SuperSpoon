using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicObject : MonoBehaviour,IObject
{
    public InteractEvent intEvent;
    public void Interact()
    {
        eventManager.Instance.startEvent(intEvent,()=> { print("Termine"); });
    }

}
