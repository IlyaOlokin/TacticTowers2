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
        bool isPositive = greenValue[0] != '-';
        string color = isPositive ? "33FF00" : "D61F1F";
        string sign = isPositive ? " + " : " - ";
        if (!isPositive) greenValue = greenValue.Substring(1);
        value.text = baseValue + "<color=#" + color + ">" + sign + greenValue + "</color>";
    }
}
