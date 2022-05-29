using UnityEngine;
using UnityEngine.Audio;
using System;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public Sound[] Sounds;

    public bool soundEnabled = true;

    private List<float> savedSoundsVolume = new List<float>();
    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        for (int i = 0; i < Sounds.Length; i++)
        {
            Sounds[i].source = gameObject.AddComponent<AudioSource>();
            Sounds[i].source.clip = Sounds[i].clip;
            Sounds[i].source.volume = Sounds[i].volume;
            savedSoundsVolume.Add(Sounds[i].volume);
            Sounds[i].source.pitch = Sounds[i].pitch;
            Sounds[i].source.loop = Sounds[i].loop;
        }
    }

    private void Start()
    {
        Play("MainTheme");
    }
    
    public void Play(string name)
    {
        Sound s = Array.Find(Sounds, sound => sound.name == name);
        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(Sounds, sound => sound.name == name);
        s.source.Stop();
    }

    public void SoundOn()
    {
        for (int i = 0; i < Sounds.Length; i++)
        {
            Sounds[i].source.volume = savedSoundsVolume[i];
        }

        soundEnabled = true;
    }
    
    public void SoundOff()
    {
        for (int i = 0; i < Sounds.Length; i++)
        {
            Sounds[i].source.volume = 0;
        }

        soundEnabled = false;
    }
}

[Serializable]
public class Sound
{
    public string name;
    
    public AudioClip clip;

    [Range(0, 1)]
    public float volume;
    [Range(0, 3)]
    public float pitch;

    public bool loop;

    [HideInInspector]
    public AudioSource source;
}
