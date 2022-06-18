using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeAcceleration : MonoBehaviour
{
    [SerializeField] private Text text;
    private readonly List<int> timeScales = new List<int>() {1, 2, 3};
    private int currentTimeScaleIndex = 0;

    void Update()
    {
        if (Time.timeScale == 0) return;
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeTimeScale(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeTimeScale(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeTimeScale(0);
        }
    }

    public void SwitchTimeScale()
    {
        ChangeTimeScale(currentTimeScaleIndex + 1);
    }

    private void ChangeTimeScale(int timeScaleIndex)
    {
        currentTimeScaleIndex = timeScaleIndex % timeScales.Count;
        int newTimeScale = timeScales[currentTimeScaleIndex];
        Time.timeScale = newTimeScale;
        text.text = newTimeScale + "X";
    }
}
