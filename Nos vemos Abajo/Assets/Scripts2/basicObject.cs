using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class basicObject : MonoBehaviour,IObject
{
  [SerializeField]  private InteractEvent intEvent;
    public string Hoverkey;
    public bool showName = true;
    MaterialPropertyBlock materialPropertyBlock;

    public virtual void EnterHover()
    {
        if (showName)
        {
            UIManager.Instance.showObjectName(Hoverkey);
        }
        materialPropertyBlock = new MaterialPropertyBlock();
        materialPropertyBlock.SetColor("_Color", Color.white);
        materialPropertyBlock.SetFloat("_isOutlined", 1.0f);
        this.GetComponent<MeshRenderer>().SetPropertyBlock(materialPropertyBlock, 1);

    }

    public virtual void Interact()
    {
        eventManager.Instance.startEvent(intEvent,()=> { print("Termine"); });
    }

    public virtual void LeaveHover()
    {
        UIManager.Instance.hideObjectName();
        materialPropertyBlock.SetFloat("_isOutlined", 0.0f);
        this.GetComponent<MeshRenderer>().SetPropertyBlock(materialPropertyBlock, 1);
    }
}
