using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerUpgrade : MonoBehaviour
{
    private float pressTimer;
    private float pressTimeLimit = 0.2f;

    private Vector2 mouseOnPos;
    private Vector2 mouseUpPos;
    private float dragDistanceLimit = 0.1f;
    
    [SerializeField] private GameObject upgradeMenu;
    [SerializeField] private Tower tower;
    [SerializeField] private GameObject upgradeWindow;

    private void Start()
    {
        upgradeMenu.SetActive(false);
        
    }

    private void OnMouseDown()
    {
        mouseOnPos = transform.position;
        pressTimer = 0;
    }

    private void OnMouseDrag()
    {
        pressTimer += Time.deltaTime;
    }

    private void OnMouseUp()
    {
        mouseUpPos = transform.position;
        if (pressTimer <= pressTimeLimit && Vector2.Distance(mouseOnPos,mouseUpPos) <= dragDistanceLimit)
        {
            OpenUpgradeMenu();
        }
    }

    private void OpenUpgradeMenu()
    {
        upgradeMenu.SetActive(true);
    }

    public void OpenUpgradeWindow()
    {
        var cost = tower.upgradeCost + tower.upgradeIncrement * tower.upgradeLevel;
        if (cost <= Money.GetMoney())
        {
            Money.TakeMoney(cost);
            tower.upgradeLevel++;
            upgradeWindow.SetActive(true);
            upgradeWindow.GetComponent<UpgradeWindow>().InitializeUpgrade(tower);
        }
    }
}
