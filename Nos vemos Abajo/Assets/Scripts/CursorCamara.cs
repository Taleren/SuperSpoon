using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorCamara : MonoBehaviour
{
    // Start is called before the first frame update
    private Camera cam;
    IObject currentObject;

    public LayerMask clickable;

    void Start()
    {
        cam = Camera.main;

       // Cursor.visible = true;
       // Cursor.lockState = CursorLockMode.Locked;
    }
    void leaveCurrent()
    {
        if (currentObject != null)
        {
            currentObject.LeaveHover();
        }
        currentObject = null;
    }
    // Update is called once per frame
    void Update()
    {
        if (gameManager.Instance.currentState != gameManager.GameState.Paused)
        {
            RaycastHit hitContinuo;
            Ray rayContinuo = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(rayContinuo, out hitContinuo, Mathf.Infinity, clickable))
            {
                IObject _obj = hitContinuo.transform.gameObject.GetComponent<IObject>();
                if (_obj == null)
                {
                    leaveCurrent();
                }
                if (_obj != currentObject)
                {
                    leaveCurrent();
                    currentObject = _obj;
                    currentObject.EnterHover();
                }

                //  Debug.Log("IIIIIIIIIIIIIIIIIIIIIIIIIIIIII");
            }
            else
            {
                leaveCurrent();

            }

            if (Input.GetMouseButtonDown(0))
            {
                if (currentObject != null)
                {
                    currentObject.Interact();
                }
                //RaycastHit hit;
                //Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                //if (Physics.Raycast(ray, out hit, Mathf.Infinity, clickable)) 
                //{
                //    IObject _obj = hit.transform.gameObject.GetComponent<IObject>();
                //    if (_obj != null)
                //    {
                //        _obj.Interact();
                //    }
                //    Debug.Log("LO ESTAS TOCANDO OOOOOOOOOOOOOOOOOOOOOOOOOOO");
                //}
            }

        }
        else
        {
            leaveCurrent();

        }

       
    }
}
