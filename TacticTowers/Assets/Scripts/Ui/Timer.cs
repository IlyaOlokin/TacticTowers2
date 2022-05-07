using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public static float timer;
    private Text text;

    private void Start()
    {
        text = GetComponent<Text>();
        SetTimer(0);
    }

    void Update()
    {
        timer += Time.deltaTime;
        var minutes = (int) timer / 60;
        var seconds = (int) timer % 60;

        var minutesText = minutes < 10
            ? "0" + minutes.ToString()
            : minutes.ToString();
        
        var secondsText = seconds < 10
            ? "0" + seconds.ToString()
            : seconds.ToString();
        
        text.text = minutesText + " : " + secondsText;
    }

    private static void SetTimer(int seconds)
    {
        timer = seconds;
    } 
}
