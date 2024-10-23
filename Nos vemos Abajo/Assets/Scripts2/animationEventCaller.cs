using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationEventCaller : MonoBehaviour
{
 [SerializeField]   GameObject[] farolHijo;
 [SerializeField]   MeshRenderer[] meshr;

    public void nextCall()
    {
        eventManager.Instance.playCall();
    }
    
    public void encenderFarol()
    {
        foreach (GameObject item in farolHijo)
        {
            item.SetActive(true);

        }
        MaterialPropertyBlock propertyBlock = new MaterialPropertyBlock();
        propertyBlock.SetFloat("Emission", 1.0f);
        foreach (MeshRenderer item in meshr)
        {
            print(item.name);
            item.SetPropertyBlock(propertyBlock);

        }
    }
}
