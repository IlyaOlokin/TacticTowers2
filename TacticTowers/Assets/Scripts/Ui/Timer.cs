using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public static float timer;
    private Text text;
    private static bool isStopped;

    private void Start()
    {
        text = GetComponent<Text>();
        isStopped = false;
        SetTimer(0);
    }

    void Update()
    {
        if (isStopped) return;
        timer -= Time.deltaTime;
        var minutes = (int) timer / 60;
        var seconds = Mathf.Ceil(timer % 60);

        text.text = $"{minutes:00}:{seconds:00}";
    }

    public static void SetTimer(int seconds)
    {
        timer = seconds;
    }

    public static void Stop()
    {
        isStopped = true;
    }
    
    public static void Play()
    {
        isStopped = false;
    }
}
