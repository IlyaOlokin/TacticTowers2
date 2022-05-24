using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalUpgrade : MonoBehaviour
{
    [SerializeField] private List<GameObject> upgradeFills;
    [SerializeField] private Text valueText;
    [SerializeField] private Text priceText;
    [SerializeField] private GameObject button;

    [SerializeField] private List<int> prices;
    [SerializeField] private float currentValue;
    [SerializeField] private float bonusValue;
    [SerializeField] private UpgradeObject upgradeObject;
    private int upgradeLevel = 0;
    
    enum UpgradeObject
    {
        BaseHp,
        Dmg,
        ShootAngle,
        Money
    }
    
    void Start()
    {
        UpdateTexts();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            Credits.credits += 100;
        }
    }

    public void Upgrade()
    {
        if (upgradeLevel == prices.Count) return;
        if (Credits.credits < prices[upgradeLevel]) return;

        Credits.credits -= prices[upgradeLevel];
        upgradeFills[upgradeLevel].SetActive(true);
        upgradeLevel++;
        currentValue += bonusValue;
        UpdateTexts();

        switch (upgradeObject)
        {
            case UpgradeObject.BaseHp:
                GlobalMultipliers.baseHpMultiplier = currentValue;
                break;
            case UpgradeObject.Dmg :
                GlobalMultipliers.dmgMultiplier = currentValue;
                break;
            case UpgradeObject.ShootAngle :
                GlobalMultipliers.shootAngleMultiplier = currentValue;
                break;
            case UpgradeObject.Money :
                GlobalMultipliers.moneyMultiplier = currentValue;
                break;
        }
    }

    private void UpdateTexts()
    {
        if (upgradeLevel == prices.Count)
        {
            priceText.text = "MAX!";
            valueText.text = currentValue * 100 + "%";
        }
        else
        {
            priceText.text = prices[upgradeLevel].ToString();
            valueText.text = currentValue * 100 + "% + " + bonusValue * 100 + "%";
        }
    }
}
