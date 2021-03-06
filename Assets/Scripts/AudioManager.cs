using System;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    private void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    private void Start()
    {
        PlayAudio("Theme");
    }

    public void PlayAudio(string naming)
    {
        Sound s = Array.Find(sounds, sound => sound.name == naming);
        if(s == null) return;
        s.source.Play();
    }
}
