using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorCamara : MonoBehaviour
{
    // Start is called before the first frame update
    private Camera cam;
    IObject currentObject;

    public LayerMask clickable;
    public LayerMask clickableminigame;
    private LayerMask currentMask;

    void Start()
    {
        cam = Camera.main;
        currentMask = clickable;
        gameManager.Instance.setChangeStateEvent(ChangeState);
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

            if (Physics.Raycast(rayContinuo, out hitContinuo, Mathf.Infinity, currentMask))
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
    public void ChangeState()
    {
        switch (gameManager.Instance.currentState)
        {
            case gameManager.GameState.Paused:
                break;
            case gameManager.GameState.FreePlay:
                currentMask = clickable;
                break;
            case gameManager.GameState.Minigame:
                currentMask = clickableminigame;

                break;
            default:
                break;
        }
    }
}
