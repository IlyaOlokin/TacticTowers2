using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerStat : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private Text statLabel;
    [SerializeField] private Text value;

    public void SetData(Sprite icon, string statLabel, string baseValue, string greenValue)
    {
        this.icon.sprite = icon;
        this.statLabel.text = statLabel;
        value.text = baseValue + "<color=#33FF00> + " + greenValue + "</color>";
    }
}
