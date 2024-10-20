using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minigame : MonoBehaviour
{
    //[SerializeField]  GameObject mainModel;

    [SerializeField] protected InteractEvent onEndEvent;
    [SerializeField] protected InteractEvent postEndEvent;

    Vector3 startPos;

    // [SerializeField] minigameObject activateObject;
    public virtual void StartMinigame()
    {
        //gameObject.SetActive(true);
        transform.position = Camera.main.transform.position + Camera.main.transform.forward * 0.5f;
        foreach (minigameObject item in GetComponentsInChildren<minigameObject>())
        {
            item.startMinigame();
        }
    }
    public void endMinigame()
    {
        transform.position = startPos;

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
        gameManager.Instance.subscribeGameStart(startGame);
       // gameObject.SetActive(false);

    }
  public void startGame()
    {
        transform.position = startPos;
    }
    


}
