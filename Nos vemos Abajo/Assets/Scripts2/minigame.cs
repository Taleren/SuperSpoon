using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minigame : MonoBehaviour
{
    GameObject mainModel;
    GameObject instantiatedModel;
    public void StartMinigame()
    {
        instantiatedModel = Instantiate(mainModel, Vector3.zero,Quaternion.identity);
    }
    public void endMinigame()
    {
        Destroy(instantiatedModel);
    }

    
}
