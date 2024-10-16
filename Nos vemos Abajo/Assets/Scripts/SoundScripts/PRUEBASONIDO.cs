using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PRUEBASONIDO : MonoBehaviour
{
    private void Start()
    {
        SoundManager.instance.PlaySound("ventisca", transform.position);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            SoundManager.instance.PlaySound("salchipapa", transform.position);
            
        }
    }
}
