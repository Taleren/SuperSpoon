using UnityEngine;
using System;

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
        /*else
        {
            Destroy(gameObject);
            return;
        }*/
        DontDestroyOnLoad(gameObject);

    }

    // Método para reproducir un sonido en una posición específica
    public void PlaySound(string soundName, Vector3 soundPosition)
    {
        Sound s = Array.Find(sounds, sound => sound.name == soundName);
        
        if (s == null)
        {
            Debug.LogWarning("Sound: " + soundName + " not found!");
            return;
        }

        // Crear un objeto temporal para el AudioSource 
        GameObject tempAudioObject = new GameObject("TempAudio");
        tempAudioObject.transform.position = soundPosition;

        // Añadir un AudioSource al objeto temporal
        AudioSource tempAudioSource = tempAudioObject.AddComponent<AudioSource>();

        // Configurar las propiedades del AudioSource
        tempAudioSource.clip = s.clip;
        tempAudioSource.volume = s.volume;
        tempAudioSource.pitch = s.pitch;
        tempAudioSource.loop = s.loop;
        tempAudioSource.spatialBlend = s.spatialBlend;

        // Reproducir el sonido
        tempAudioSource.Play();

        // Si el sonido no está en loop, destruir el objeto después de que el clip haya terminado
        if (!s.loop)
        {
            Destroy(tempAudioObject, s.clip.length);
        }
    }

}

