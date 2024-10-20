using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IObject 
{
    public void Interact();
    public void EnterHover();
    public void LeaveHover();

    public basicObject.ObjState getState();

}
