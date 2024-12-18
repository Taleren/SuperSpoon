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

    public List<GameObject> ObjectsToSerilialize;

 [SerializeField]   Transform fatherSoundPointers;

    

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
        director.Stop();
        gameManager.Instance.setCameraSpeed(true);

        currentNextAction?.Invoke();
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
    public void callSound(string name,int val)
    {
        print(name + val + "sonido");
        SoundManager.instance.PlaySound(name, fatherSoundPointers.transform.GetChild(val).transform.position, fatherSoundPointers.transform.GetChild(val).transform.gameObject);
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
    public void timelineDialogue(string dialog,int end)
    {
        TextManager.Instance.playDialogue(dialog, () => { });

    }
    //public void soundDialogue(string soundKey,int objectNum)
    //{
    //   // SoundManager.instance.PlaySound();
    //}
    public void soundDialogueSimple(string soundKey)
    {
        SoundManager.instance.PlaySound(soundKey, Vector3.zero);
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
