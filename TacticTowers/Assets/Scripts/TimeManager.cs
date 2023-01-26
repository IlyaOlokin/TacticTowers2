using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public static class TimeManager
{
    private static float savedTimeScale = 1f;
    
    public static void Resume(AudioMixer audioMixer)
    {
        Time.timeScale = savedTimeScale;
        audioMixer.SetFloat("EnvVol", 0.0f);
    }

    public static void Pause(AudioMixer audioMixer)
    {
        savedTimeScale = Time.timeScale;
        Time.timeScale = 0;
        audioMixer.SetFloat("EnvVol", -80.0f);

    }

    public static void SetTimeScale(float newTimeScale)
    {
        Time.timeScale = newTimeScale;
        savedTimeScale = newTimeScale;
    }
}
