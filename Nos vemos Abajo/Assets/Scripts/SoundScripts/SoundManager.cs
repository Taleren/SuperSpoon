using UnityEngine;
using System;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public Sound[] sounds;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

    }
   
    public void PlaySound(string soundName, Vector3 soundPosition, GameObject parent = null, Action action = null)
    {
        Sound s = Array.Find(sounds, sound => sound.name == soundName);

        GameObject tempAudioObject = new GameObject("TemporalAudio");
        tempAudioObject.transform.position = soundPosition;

        if (parent != null)
        {
            tempAudioObject.transform.SetParent(parent.transform);
        }

        AudioSource tempAudioSource = tempAudioObject.AddComponent<AudioSource>();

        tempAudioObject.GetComponent<AudioSource>().outputAudioMixerGroup = s.audioMixerGroup;
        tempAudioSource.clip = s.clip;
        tempAudioSource.volume = s.volume;
        tempAudioSource.pitch = s.pitch;
        tempAudioSource.loop = s.loop;
        tempAudioSource.spatialBlend = s.spatialBlend;

        tempAudioSource.Play();
        if(action != null)
        {
            StartCoroutine(waitForAction(action, s.clip.length));
        }

        if (!s.loop)
        {
            Destroy(tempAudioObject, s.clip.length);
        }
    }
    IEnumerator waitForAction(Action action,float time)
    {
     yield return new   WaitForSeconds(time);
        action?.Invoke();
    }

    public float duracionSonido(string soundName)
    {
        float duracion;
        Sound sonido = Array.Find(sounds, sound => sound.name == soundName);
        duracion = sonido.clip.length;
        return duracion;
    }

    public Sound buscarSonido(string soundName)
    {
        Sound sonido = Array.Find(sounds, sound => sound.name == soundName);
        return sonido;
    }
}

