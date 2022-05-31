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
    public List<Laser> lasers;
    public List<Flamethrower> flamethrowers;
    public List<Frostgun> frostguns;
    
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
            Sounds[i].source.clip = Sounds[i].clip;
            Sounds[i].source.volume = Sounds[i].volume;
            savedSoundsVolume.Add(Sounds[i].volume);
            Sounds[i].source.pitch = Sounds[i].pitch;
            Sounds[i].source.loop = Sounds[i].loop;
        }
    }

    private void Start()
    {
        if (Convert.ToBoolean(PlayerPrefs.GetInt("isMusicOn", 1)))
            Play("MainTheme");
    }

    private void Update()
    {
        ControlLoopSound("LaserShot", IsAnyLaserShooting());
        ControlLoopSound("FrostgunShot", IsAnyFrostgunShooting());
        ControlLoopSound("FlamethrowerShot", IsAnyFlamethrowerShooting());
    }

    private void ControlLoopSound(string name, bool isAnyShooting)
    {
        if (Time.timeScale == 0)
        {
            Stop(name);
            return;
        }
        bool isPlaying = Array.Find(Sounds, sound => sound.name == name).source.isPlaying;
        if (isAnyShooting && !isPlaying)
            Play(name);
        else if (!isAnyShooting)
            Stop(name);
    }

    private bool IsAnyLaserShooting()
    {
        foreach (var laser in lasers)
        {
            if (laser == null)
            {
                lasers.Remove(laser);
                return false;
            }
            if (laser.shooting) return true;
        }

        return false;
    }
    
    private bool IsAnyFrostgunShooting()
    {
        foreach (var frostgun in frostguns)
        {
            if (frostgun == null)
            {
                frostguns.Remove(frostgun);
                return false;
            }
            if (frostgun.shooting) return true;
        }

        return false;
    }
    
    private bool IsAnyFlamethrowerShooting()
    {
        foreach (var flamethrower in flamethrowers)
        {
            if (flamethrower == null)
            {
                flamethrowers.Remove(flamethrower);
                return false;
            }
            if (flamethrower.shooting) return true;
        }

        return false;
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

    [HideInInspector]
    public AudioSource source;
}
