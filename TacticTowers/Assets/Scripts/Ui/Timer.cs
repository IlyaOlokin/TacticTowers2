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
        timer -= Time.deltaTime;
        var minutes = (int) timer / 60;
        var seconds = Mathf.Ceil(timer % 60);

        text.text = $"{minutes:00}:{seconds:00}";
    }

    public static void SetTimer(int seconds)
    {
        timer = seconds;
    } 
}
