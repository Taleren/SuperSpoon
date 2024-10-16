using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField] private AudioSource soundObject;

    public AudioClip salchipapa;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void  PlaySound(AudioClip audioClip, Transform spawn, float volume) 
    {
        AudioSource audioSource = Instantiate(soundObject, spawn.position, Quaternion.identity);

        audioSource.clip = audioClip;
        audioSource.volume = volume;    
        audioSource.Play();

        float clipLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLength);
    }
}
