using UnityEngine;
using UnityEngine.Audio;
using System;
using System.Collections.Generic;

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

    private void Start()
    {
        if (Convert.ToBoolean(DataLoader.LoadInt("isMusicOn", 1)))
            Play("MainTheme");
    }

    public void Play(string name)
    {
        Sound s = Array.Find(Sounds, sound => sound.name == name);
        s.source.PlayOneShot(s.source.clip);
        //s.source.Play();
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
            if (Sounds[i].name == "MainTheme")
                continue;
            
            Sounds[i].source.volume = savedSoundsVolume[i];
        }

        soundEnabled = true;
    }
    
    public void SoundOff()
    {
        for (int i = 0; i < Sounds.Length; i++)
        {
            if (Sounds[i].name == "MainTheme")
                continue;
            
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
    
    public AudioMixerGroup audioMixerGroup;
    
    [HideInInspector]
    public AudioSource source;
}
