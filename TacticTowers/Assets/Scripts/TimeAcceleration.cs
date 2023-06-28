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
    private readonly List<float> timeScales = new List<float>() {1f, 1.5f, 2f};
    private static int currentTimeScaleIndex = 0;

    private void Start()
    {
        ChangeTimeScale(0);
        ColorArrows(currentTimeScaleIndex);
    }

    void Update()
    {
        if (Time.timeScale == 0) return;
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            OnButtonChangeTimeScale(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            OnButtonChangeTimeScale(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            OnButtonChangeTimeScale(0);
        }
    }

    public void SwitchTimeScale()
    {
        OnButtonChangeTimeScale(currentTimeScaleIndex + 1);
    }

    private void OnButtonChangeTimeScale(int timeScaleIndex)
    {
        ChangeTimeScale(timeScaleIndex);
        AudioManager.Instance.Play("ButtonClick1");
    }

    private void ChangeTimeScale(int timeScaleIndex)
    {
        currentTimeScaleIndex = timeScaleIndex % timeScales.Count;
        float newTimeScale = timeScales[currentTimeScaleIndex];
        TimeManager.SetTimeScale(newTimeScale);
        ColorArrows(currentTimeScaleIndex);
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
