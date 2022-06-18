using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour
{
    public bool mouseOn;
    [SerializeField] private Text towerLevel;
    [SerializeField] private Text towerLevelConst;
    [SerializeField] private Text nextUpgradeCost;
    [SerializeField] private Animation animation;
    
    private void OnEnable()
    {
        animation = GetComponent<Animation>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && !mouseOn)
        {
            DeactivateMenu();
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
        towerLevelConst.text = level.ToString();
        if (cost == 0) 
            nextUpgradeCost.text = "MAX!";
        else
            nextUpgradeCost.text = cost + "$";
        
    }

    public void ActivateMenu()
    {
        animation.Stop("UpgradeMenuAnimation");
        animation.Play("UpgradeMenuAnimation");
    }
    
    private void DeactivateMenu()
    {
        gameObject.SetActive(false);
    }
}
