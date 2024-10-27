using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CursorCamara : MonoBehaviour
{
    // Start is called before the first frame update
    private Camera cam;
    IObject currentObject;

    public LayerMask clickable;
    public LayerMask clickableminigame;
    private LayerMask currentMask;
    public LayerMask none;
    RaycastHit hitContinuo;

    [SerializeField] Sprite[] cursorSprites;

    [SerializeField] private Image cursor;
    void Start()
    {
        cam = Camera.main;
        currentMask = clickable;
        gameManager.Instance.setChangeStateEvent(ChangeState);
        cursor.enabled = false;
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
           Ray rayContinuo = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(rayContinuo, out hitContinuo, Mathf.Infinity, currentMask))
            {
                IObject _obj = hitContinuo.transform.gameObject.GetComponent<IObject>();
                if (_obj == null || _obj.getState() == basicObject.ObjState.Off)
                {
                    leaveCurrent();
                }
                if (_obj != currentObject && _obj.getState() != basicObject.ObjState.Off)
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

            if (Input.GetMouseButtonUp(0))
            {
                if (currentObject != null)
                {
                    gameManager.Instance.lastpressPos = hitContinuo.point;

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
                cursor.enabled = false;
                break;
            case gameManager.GameState.FreePlay:
                cursor.enabled = true;
                cursor.sprite = cursorSprites[0];
                currentMask = clickable;
                break;
            case gameManager.GameState.Minigame:
                cursor.enabled = false;
                cursor.sprite = cursorSprites[0];
                currentMask = clickableminigame;

                break;
            case gameManager.GameState.onInteract:
                cursor.enabled = true;
                cursor.sprite = cursorSprites[1];

                currentMask = none;

                break;
            default:
                break;
        }
    }
}
