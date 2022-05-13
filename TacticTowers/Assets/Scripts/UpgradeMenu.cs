using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour
{
    public bool mouseOn;
    [SerializeField] private Text towerLevel;
    [SerializeField] private Text nextUpgradeCost;
    
    
    void Update()
    {
        if (Input.GetMouseButton(0) && !mouseOn)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnMouseEnter()
    {
        mouseOn = true;
    }

    private void OnMouseExit()
    {
        mouseOn = false;
    }

    public void UpdateTexts(int level, int cost)
    {
        towerLevel.text = level.ToString();
        if (cost == 0) 
            nextUpgradeCost.text = "MAX!";
        else
            nextUpgradeCost.text = cost + "$";
        
    }
}
