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
    
    // Método para reproducir un sonido en una posición específica
    public void PlaySound(string soundName, Vector3 soundPosition, GameObject parent = null, Action action = null)
    {
        Sound s = Array.Find(sounds, sound => sound.name == soundName);

        // Crear un objeto temporal para el AudioSource 
        GameObject tempAudioObject = new GameObject("TemporalAudio");
        tempAudioObject.transform.position = soundPosition;

        // Si se ha pasado un padre, asignar el objeto temporal como hijo de ese padre
        if (parent != null)
        {
            tempAudioObject.transform.SetParent(parent.transform);
        }

        // Añadir un AudioSource al objeto temporal
        AudioSource tempAudioSource = tempAudioObject.AddComponent<AudioSource>();

        // Configurar las propiedades del AudioSource
        tempAudioObject.GetComponent<AudioSource>().outputAudioMixerGroup = s.audioMixerGroup;
        tempAudioSource.clip = s.clip;
        tempAudioSource.volume = s.volume;
        tempAudioSource.pitch = s.pitch;
        tempAudioSource.loop = s.loop;
        tempAudioSource.spatialBlend = s.spatialBlend;

        // Reproducir el sonido
        tempAudioSource.Play();
        if(action != null)
        {
            StartCoroutine(waitForAction(action, s.clip.length));
        }
        // Si el sonido no está en loop, destruir el objeto después de que el clip haya terminado
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

