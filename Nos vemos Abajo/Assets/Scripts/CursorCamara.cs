using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorCamara : MonoBehaviour
{
    // Start is called before the first frame update
    private Camera cam;

    public LayerMask clickable;

    void Start()
    {
        cam = Camera.main;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hitContinuo;
        Ray rayContinuo = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(rayContinuo, out hitContinuo, Mathf.Infinity, clickable))
        {
            Debug.Log("IIIIIIIIIIIIIIIIIIIIIIIIIIIIII");
        }

        if (Input.GetMouseButtonDown(0)) 
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, clickable)) 
            {
                Debug.Log("LO ESTAS TOCANDO OOOOOOOOOOOOOOOOOOOOOOOOOOO");
            }
        }
    }
}
