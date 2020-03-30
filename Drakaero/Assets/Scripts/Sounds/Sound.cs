using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    // Sound Name
    public string name;
    // Get sound file
    public AudioClip clip;
    // Control sound volume
    [Range(0f, 1f)]
    public float volume;
    // Control sound pitch
    [Range(.1f, 3f)]
    public float pitch;
    // Loop sound
    public bool loop;

    [HideInInspector]   // Don't show in the inspector
    public AudioSource source;

}
