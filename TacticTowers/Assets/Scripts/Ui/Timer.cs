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
        text.text = (int)timer / 60 + ":" + (int)timer % 60;
    }

    private static void SetTimer(int seconds)
    {
        timer = seconds;
    } 
}
