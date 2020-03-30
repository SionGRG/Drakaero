using UnityEngine.Audio;
using UnityEngine;
using System;

public class SoundManager : MonoBehaviour
{
    public Sound[] sounds;

    public static SoundManager instance;

    void Awake()
    {
        // Only create one Sound manager per scene
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        // don't destroy, if already available
        DontDestroyOnLoad(gameObject);

        // Look through the sounds
        foreach (Sound snd in sounds)
        {
            snd.source = gameObject.AddComponent<AudioSource>();
            snd.source.clip = snd.clip;
            snd.source.volume = snd.volume;
            snd.source.pitch = snd.pitch;
            snd.source.loop = snd.loop;
        }
    }

    void Start()
    {
        Play("Theme");      // play game theme
    }

    public void Play(string name)
    {
        // Find the sound in the sounds array
        Sound snd = Array.Find(sounds, sound => sound.name == name);
        // Show an error when the sound is not found
        if (snd == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        snd.source.Play();  // Play sound
    }
}
