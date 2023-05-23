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
    [SerializeField] private GameObject towerStatWindow;
    public Tower tower;
    [SerializeField] private GameObject upgradeWindow;
    private TowerDrag td;
    
    [SerializeField] private GameObject upgradeArrow;


    private void Start()
    {
        upgradeMenu.SetActive(false);
        td = GetComponent<TowerDrag>();
    }

    private void Update()
    {
        var cost = GetTowerUpgradePrice();
        if (cost == 0)
        {
            upgradeArrow.SetActive(false);
            return;
        }
        upgradeArrow.SetActive(cost <= Money.GetMoney());
    }

    private void OnMouseDown()
    {
        if (td.dragging) return;

        mouseOnPos = transform.position;
        pressTimer = 0;
    }

    private void OnMouseDrag()
    {
        pressTimer += Time.unscaledDeltaTime;
    }

    private void OnMouseUp()
    {
        if (UiAppear.IsAnyUIActive()) return;
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
        if (IsTowerMaxLevel()) return;
        var cost = GetTowerUpgradePrice();
        
        if (cost <= Money.GetMoney())
        {
            Money.TakeMoney(cost);
            tower.upgradeLevel++;
            upgradeWindow.SetActive(true);
            upgradeMenu.GetComponent<UpgradeMenu>().UpdateTexts(tower.upgradeLevel, GetTowerUpgradePrice());
            upgradeMenu.SetActive(false);
            upgradeWindow.GetComponent<UpgradeWindow>().UpgradeTower(tower);
            upgradeWindow.GetComponent<UpgradeWindow>().td = GetComponent<TowerDrag>();
            upgradeWindow.GetComponent<UpgradeWindow>().tu = this;
            
            FindObjectOfType<AudioManager>().Play("ButtonClick1");
        }
    }
    public void OpenTowerStatWindow()
    {
        towerStatWindow.SetActive(true);
        upgradeMenu.SetActive(false);
        towerStatWindow.GetComponent<TowerStatWindow>().SetTower(GetComponent<TowerDrag>().tower);
            
        FindObjectOfType<AudioManager>().Play("ButtonClick1");
    }
    
    private int GetTowerUpgradePrice()
    {
        if (IsTowerMaxLevel())
            return 0;
        return Tower.upgradePrices[tower.upgradeLevel - 1];
    }

    private bool IsTowerMaxLevel()
    {
        return tower.upgradeLevel == Tower.upgradePrices.Length + 1;
    }
}
