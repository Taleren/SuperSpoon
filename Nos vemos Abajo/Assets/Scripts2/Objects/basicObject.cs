using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class basicObject : MonoBehaviour,IObject
{
  [SerializeField] protected InteractEvent intEvent;
  [SerializeField] private GameObject outline;
    public string Hoverkey;
    public bool showName = true;
    MaterialPropertyBlock materialPropertyBlock;


    public virtual void EnterHover()

    {
        if (showName)
        {
            UIManager.Instance.showObjectName(Hoverkey);
        }
        //materialPropertyBlock = new MaterialPropertyBlock();
        //materialPropertyBlock.SetColor("_Color", Color.white);
        //materialPropertyBlock.SetFloat("_isOutlined", 1.0f);
        //this.GetComponent<MeshRenderer>().SetPropertyBlock(materialPropertyBlock, 1);
        if (outline != null)
        {
            outline.SetActive(true);
        }
    }

    public virtual void Interact()
    {
        eventManager.Instance.startEvent(intEvent,()=> { print("Termine"); });
        LeaveHover();
    }

    public virtual void LeaveHover()
    {
        UIManager.Instance.hideObjectName();
        /*
        materialPropertyBlock.SetFloat("_isOutlined", 0.0f);
        this.GetComponent<MeshRenderer>().SetPropertyBlock(materialPropertyBlock, 1);
        */
        if (outline != null)
        {
            outline.SetActive(false);
        }
    }
}
