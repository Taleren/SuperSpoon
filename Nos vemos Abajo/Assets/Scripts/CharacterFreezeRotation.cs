using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class CharacterFreezeRotation : MonoBehaviour
{
    [SerializeField] GameObject camera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       float cameraY = camera.transform.eulerAngles.y;
       Vector3 rot = new Vector3(0f, cameraY+90, 0f);
        this.transform.eulerAngles = rot;

    }
}
