using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicObject : MonoBehaviour,IObject
{
    public InteractEvent intEvent;
    public string Hoverkey;
    public void EnterHover()
    {
        UIManager.Instance.showObjectName(Hoverkey);
    }

    public void Interact()
    {
        eventManager.Instance.startEvent(intEvent,()=> { print("Termine"); });
    }

    public void LeaveHover()
    {
        UIManager.Instance.hideObjectName();
    }
}
