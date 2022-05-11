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
    public Tower tower;
    [SerializeField] private GameObject upgradeWindow;
    private TowerDrag td;

    private void Start()
    {
        upgradeMenu.SetActive(false);
        td = GetComponent<TowerDrag>();
    }

    private void OnMouseDown()
    {
        if (td.dragging) return;

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
        if (td.dragging) return;
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
            upgradeWindow.GetComponent<UpgradeWindow>().UpgradeTower(tower);
            upgradeWindow.GetComponent<UpgradeWindow>().td = GetComponent<TowerDrag>();
            upgradeWindow.GetComponent<UpgradeWindow>().tu = this;
        }
    }
}
