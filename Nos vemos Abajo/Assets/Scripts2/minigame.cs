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
  [SerializeField]  Vector3 startRot;
    // [SerializeField] minigameObject activateObject;
    public virtual void StartMinigame()
    {
        //gameObject.SetActive(true);
        transform.position = Camera.main.transform.position + Camera.main.transform.forward * 0.5f;
        transform.eulerAngles = startRot;
        eventManager.Instance.startEvent(startEvent, () => { gameManager.Instance.setState(gameManager.GameState.FreePlay); });

        foreach (minigameObject item in GetComponentsInChildren<minigameObject>())
        {
            item.startMinigame();
        }
    }
    public void endMinigame()
    {
        transform.position = startPos;
        transform.eulerAngles = startRot;

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
        startRot = transform.eulerAngles;
        gameManager.Instance.subscribeGameStart(startGame);
       // gameObject.SetActive(false);

    }
  public void startGame()
    {
       
    }
    


}
