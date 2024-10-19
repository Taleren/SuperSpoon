using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Sound
{
    public string name; // Nombre del sonido para identificarlo
    public AudioClip clip; // El clip de audio asociado

    [Range(0f, 1f)]
    public float volume = 1f; // El volumen del sonido

    [Range(0f, 1f)]
    public float spatialBlend = 1f; // El "3D" del sonido

    [Range(0.1f, 3f)]
    public float pitch = 1f; // El pitch o "tono" del sonido

    public bool loop; // Si el sonido debe repetirse en bucle

    [HideInInspector]
    public AudioSource source; // El AudioSource que reproducirá este sonido
}
