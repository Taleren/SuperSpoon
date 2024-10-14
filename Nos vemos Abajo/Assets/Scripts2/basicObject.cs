using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicObject : MonoBehaviour,IObject
{
    public InteractEvent intEvent;
    public string Hoverkey;
    MaterialPropertyBlock materialPropertyBlock;
    public void EnterHover()
    {
        UIManager.Instance.showObjectName(Hoverkey);
        materialPropertyBlock = new MaterialPropertyBlock();
        materialPropertyBlock.SetColor("_Color", Color.white);
        materialPropertyBlock.SetFloat("_isOutlined", 1.0f);
        this.GetComponent<MeshRenderer>().SetPropertyBlock(materialPropertyBlock, 1);

    }

    public void Interact()
    {
        eventManager.Instance.startEvent(intEvent,()=> { print("Termine"); });
    }

    public void LeaveHover()
    {
        UIManager.Instance.hideObjectName();
        materialPropertyBlock.SetFloat("_isOutlined", 0.0f);
        this.GetComponent<MeshRenderer>().SetPropertyBlock(materialPropertyBlock, 1);
    }
}
