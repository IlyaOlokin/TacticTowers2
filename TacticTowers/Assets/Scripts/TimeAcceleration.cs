using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeAcceleration : MonoBehaviour
{
    [SerializeField] private List<GameObject> arrows;
    [SerializeField] private Color arrowSelectedColor;
    [SerializeField] private Color arrowDefaultColor;
    private readonly List<int> timeScales = new List<int>() {1, 2, 4};
    private static int currentTimeScaleIndex = 0;

    private void Start()
    {
        ColorArrows(currentTimeScaleIndex);
    }

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
        TimeManager.SetTimeScale(newTimeScale);
        ColorArrows(currentTimeScaleIndex);
        AudioManager.Instance.Play("ButtonClick1");
    }

    private void ColorArrows(int timeScaleIndex)
    {
        for (int i = 0; i < arrows.Count; i++)
        {
            if (i < timeScaleIndex)
            {
                arrows[i].GetComponent<Image>().color = arrowSelectedColor;
            }
            else
            {
                arrows[i].GetComponent<Image>().color = arrowDefaultColor;
            }
        }
    }
}
