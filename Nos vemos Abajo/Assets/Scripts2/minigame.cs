using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minigame : MonoBehaviour
{
    //[SerializeField]  GameObject mainModel;

    [SerializeField] protected InteractEvent onEndEvent;

    // [SerializeField] minigameObject activateObject;
    public virtual void StartMinigame()
    {
        gameObject.SetActive(true);
        transform.position = Camera.main.transform.position + Camera.main.transform.forward * 0.5f;
        foreach (minigameObject item in GetComponentsInChildren<minigameObject>())
        {
            item.startMinigame();
        }
    }
    public void endMinigame()
    {
        gameObject.SetActive(false);
        miniGameManager.Instance.endMinigame();
    }
    //public void nextState()
    //{
    //    activateObject.setState(minigameObject.ObjState.Active);
    //}
    private void Start()
    {
        gameObject.SetActive(false);

    }


}
