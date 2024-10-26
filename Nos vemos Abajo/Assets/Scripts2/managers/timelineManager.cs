using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using System;
using UnityEditor;
using Object = UnityEngine.Object;

public class timelineManager : MonoBehaviour, IExposedPropertyTable
{
    public static timelineManager Instance;
    [SerializeField] private PlayableDirector director;
    Action currentNextAction;
    GameObject obj;
    [SerializeField] GameObject pointerSound;

    public List<GameObject> ObjectsToSerilialize;

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
        gameManager.Instance.setCameraSpeed(false);

        currentNextAction = _action;
        director.playableAsset = timeline;
        director.Play();
    }
    public void endTimeline()
    {
        gameManager.Instance.setCameraSpeed(true);

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
    public void parenting(ExposedReference<GameObject> obj, ExposedReference<GameObject> parent)
    {
        obj.Resolve(this).transform.SetParent(parent.Resolve(this).transform);
        
    }
    public void callSound(string name)
    {
        SoundManager.instance.PlaySound(name,pointerSound.transform.position);
    }
    public List<PropertyName> listPropertyName;
    public List<UnityEngine.Object> listReference;
    

    public void ClearReferenceValue(PropertyName id)
    {
        int index = listPropertyName.IndexOf(id);
        if (index != -1)
        {
            listReference.RemoveAt(index);
            listPropertyName.RemoveAt(index);
        }
    }
    public void timelineDialogue(string dialog)
    {
        TextManager.Instance.playDialogue(dialog, ()=> { });
    }
    public Object GetReferenceValue(PropertyName id, out bool idValid)
    {
        int index = listPropertyName.IndexOf(id);
        if (index != -1)
        {
            idValid = true;
            return listReference[index];
        }
        idValid = false;
        return null;
    }

    public void SetReferenceValue(PropertyName id, UnityEngine.Object value)
    {
        int index = listPropertyName.IndexOf(id);
        if (index != -1)
        {
            listReference[index] = value;
        }
        else
        {
            listPropertyName.Add(id);
            listReference.Add(value);
        }
    }

}
