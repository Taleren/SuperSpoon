using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using System;

public class timelineManager : MonoBehaviour
{
    public static timelineManager Instance;
    [SerializeField] private PlayableDirector director;
    Action currentNextAction;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    public void callTimeline(TimelineAsset timeline, Action _action)
    {
        currentNextAction = _action;
        director.playableAsset = timeline;
        director.Play();
    }
    public void endTimeline()
    {
        currentNextAction.Invoke();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
