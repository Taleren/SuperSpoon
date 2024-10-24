using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

[Serializable]
public class eventCalls
{


    public InteractEvent.eventCallTypes callType;
    public string nameKey;
    public TimelineAsset timelineObj;
    public Transform Transform;
    public bool Boolean;
    public float waitTime;
        public bool PlayWithBefore;
    public GameObject obj;
    public Animator animator;
    [SerializeField, SerializeReference] public basicObject interactObject;
    public basicObject.ObjState newState;

}
