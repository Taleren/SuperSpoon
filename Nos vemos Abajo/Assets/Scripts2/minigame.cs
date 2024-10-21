using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class minigame : MonoBehaviour
{
    //[SerializeField]  GameObject mainModel;

    [SerializeField] protected InteractEvent startEvent;

    [SerializeField] protected InteractEvent onEndEvent;

 [SerializeField]   Vector3 startPos;
  [SerializeField]  Quaternion startRot;
    // [SerializeField] minigameObject activateObject;
    public virtual void StartMinigame()
    {
        //gameObject.SetActive(true);
        transform.position = Camera.main.transform.position + Camera.main.transform.forward * 0.5f;
        eventManager.Instance.startEvent(startEvent, () => { });

        foreach (minigameObject item in GetComponentsInChildren<minigameObject>())
        {
            item.startMinigame();
        }
    }
    public void endMinigame()
    {
        transform.position = startPos;
        transform.rotation = startRot;

        // gameObject.SetActive(false);
        miniGameManager.Instance.endMinigame(onEndEvent);
    }
    //public void nextState()
    //{
    //    activateObject.setState(minigameObject.ObjState.Active);
    //}
    private void Start()
    {
        startPos = transform.position;
        startRot = transform.rotation;
        gameManager.Instance.subscribeGameStart(startGame);
       // gameObject.SetActive(false);

    }
  public void startGame()
    {
        transform.position = startPos;
        transform.rotation = startRot;
    }
    


}
