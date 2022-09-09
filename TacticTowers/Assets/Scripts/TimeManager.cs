using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TimeManager
{
    private static float savedTimeScale = 1f;
    
    public static void Resume()
    {
        Time.timeScale = savedTimeScale;
    }

    public static void Pause()
    {
        savedTimeScale = Time.timeScale;
        Time.timeScale = 0;
    }

    public static void SetTimeScale(float newTimeScale)
    {
        Time.timeScale = newTimeScale;
        savedTimeScale = newTimeScale;
    }
}
