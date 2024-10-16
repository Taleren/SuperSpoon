using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minigame : MonoBehaviour
{
  //[SerializeField]  GameObject mainModel;

   [SerializeField] minigameObject activateObject;
    public void StartMinigame()
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
    public void nextState()
    {
        activateObject.setState(minigameObject.miniGameObjState.Active);
    }
    private void Start()
    {
        gameObject.SetActive(false);

    }


}
