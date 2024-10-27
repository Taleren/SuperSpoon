using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class multipleTapMinigame : minigame
{
    [SerializeField] int numberTaps;
    private int currentTaps;
    [SerializeField] protected InteractEvent onFinishTap;
    [SerializeReference] bool endOnTap = false;
    [SerializeField] MeshRenderer[] meshr;
    [SerializeField] float startSnow;
    float currentSnow;
    [SerializeField] float endSnow;
    [SerializeField] GameObject particles;

    public override void StartMinigame()
    {
        base.StartMinigame();
        currentSnow = startSnow;
        currentTaps = 0;
        if (meshr.Length > 0)
        {
            MaterialPropertyBlock propertyBlock = new MaterialPropertyBlock();
            propertyBlock.SetFloat("_Snow_Quantity", startSnow);

            foreach (MeshRenderer item in meshr)
            {
                print(item.name);
                item.SetPropertyBlock(propertyBlock);

            }
        }
    }
    public void Tap()
    {
        currentTaps++;
        if(currentTaps>= numberTaps)
        {
            if (endOnTap)
            {
                eventManager.Instance.startEvent(onFinishTap, () => { this.endMinigame(); });

            }
            else
            {
                eventManager.Instance.startEvent(onFinishTap, () => { gameManager.Instance.setState(gameManager.GameState.FreePlay); });
            }
        }
    }
    public void changeSnow()
    {
        MaterialPropertyBlock propertyBlock = new MaterialPropertyBlock();
        GameObject newpart = Instantiate(particles, gameManager.Instance.lastpressPos,Quaternion.identity);
        currentSnow = currentSnow + ((endSnow - startSnow) / numberTaps);
        print(currentSnow + ((endSnow - startSnow) / numberTaps));
        propertyBlock.SetFloat("_Snow_Quantity", currentSnow);
        Destroy(newpart, 3);
        foreach (MeshRenderer item in meshr)
        {
            print(item.name);
            item.SetPropertyBlock(propertyBlock);

        }
    }
}
