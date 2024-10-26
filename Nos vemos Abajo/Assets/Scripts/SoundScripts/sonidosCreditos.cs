using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sonidosCreditos : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        SoundManager.instance.PlaySound("susto", new Vector3(1,1,1));
    }

    
}
