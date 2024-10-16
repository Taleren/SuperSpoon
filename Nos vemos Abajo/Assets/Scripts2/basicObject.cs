using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class basicObject : MonoBehaviour,IObject
{
  [SerializeField] private InteractEvent intEvent;
  [SerializeField] private GameObject outline;
    public string Hoverkey;
<<<<<<< Updated upstream
    public bool showName = true;
    MaterialPropertyBlock materialPropertyBlock;


    public virtual void EnterHover()
=======

    public void EnterHover()
>>>>>>> Stashed changes
    {
        if (showName)
        {
            UIManager.Instance.showObjectName(Hoverkey);
        }
        //materialPropertyBlock = new MaterialPropertyBlock();
        //materialPropertyBlock.SetColor("_Color", Color.white);
        //materialPropertyBlock.SetFloat("_isOutlined", 1.0f);
        //this.GetComponent<MeshRenderer>().SetPropertyBlock(materialPropertyBlock, 1);
        outline.SetActive(true);

    }

    public virtual void Interact()
    {
        eventManager.Instance.startEvent(intEvent,()=> { print("Termine"); });
    }

    public virtual void LeaveHover()
    {
        UIManager.Instance.hideObjectName();
        /*
        materialPropertyBlock.SetFloat("_isOutlined", 0.0f);
        this.GetComponent<MeshRenderer>().SetPropertyBlock(materialPropertyBlock, 1);
        */
        outline.SetActive(false);
    }
}
