using UnityEngine;
using UnityEngine.Audio;
using System;
using System.Collections.Generic;
using System.Linq;

public class AudioManager : MonoBehaviour
{
    private List<float> savedSoundsVolume = new List<float>();
    public Sound[] Sounds;
    public bool soundEnabled = true;
    
    public static AudioManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        
        DontDestroyOnLoad(gameObject);

        for (int i = 0; i < Sounds.Length; i++)
        {
            Sounds[i].source = gameObject.AddComponent<AudioSource>();
            Sounds[i].source.outputAudioMixerGroup = Sounds[i].audioMixerGroup;
            Sounds[i].source.clip = Sounds[i].clip;
            Sounds[i].source.volume = Sounds[i].volume;
            Sounds[i].source.pitch = Sounds[i].pitch;
            Sounds[i].source.loop = Sounds[i].loop;
            savedSoundsVolume.Add(Sounds[i].volume);
        }
    }

    public void Play(string name)
    {
        Array.Find(Sounds, sound => sound.name == name).source.Play();
    }

    public void Stop(string name)
    {
        Array.Find(Sounds, sound => sound.name == name).source.Stop();
    }

    public void PlayMusic(string name)
    {
        Sounds
            .Where(sound => sound.source.isPlaying && sound.isMusic)
            .ToList()
            .ForEach(sound => sound.source.Stop());
        Play(name);
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
    public bool isMusic;
    
    public AudioMixerGroup audioMixerGroup;
    
    [HideInInspector]
    public AudioSource source;
}
