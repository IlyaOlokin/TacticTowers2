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
        LoadValues();
        
        SetUpgradeVisuals(upgradeLevel);
        UpdateTexts();
    }

    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            Credits.credits += 100;
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            PlayerPrefs.DeleteAll();
        }
    }

    public void Upgrade()
    {
        if (upgradeLevel == prices.Count) return;
        if (!HaveEnoughMoney()) return;
        
        Credits.TakeCredits(prices[upgradeLevel]);
        currentValue += bonusValue;
        upgradeLevel++;
        SetUpgradeVisuals(upgradeLevel);

        switch (upgradeObject)
        {
            case UpgradeObject.BaseHp:
                GlobalMultipliers.baseHpMultiplier = currentValue;
                PlayerPrefs.SetString("baseHpMultiplier", currentValue.ToString());
                PlayerPrefs.SetString("baseHpMultiplierUpgradeLevel", upgradeLevel.ToString());
                break;
            case UpgradeObject.Dmg :
                GlobalMultipliers.dmgMultiplier = currentValue;
                PlayerPrefs.SetString("dmgMultiplier", currentValue.ToString());
                PlayerPrefs.SetString("dmgMultiplierUpgradeLevel", upgradeLevel.ToString());

                break;
            case UpgradeObject.ShootAngle :
                GlobalMultipliers.shootAngleMultiplier = currentValue;
                PlayerPrefs.SetString("shootAngleMultiplier", currentValue.ToString());
                PlayerPrefs.SetString("shootAngleMultiplierUpgradeLevel", upgradeLevel.ToString());

                break;
            case UpgradeObject.Money :
                GlobalMultipliers.moneyMultiplier = currentValue;
                PlayerPrefs.SetString("moneyMultiplier", currentValue.ToString());
                PlayerPrefs.SetString("moneyMultiplierUpgradeLevel", upgradeLevel.ToString());

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

    private void SetUpgradeVisuals(int upgradeLevel)
    {
        UpdateTexts();
        for (int i = 0; i < upgradeFills.Count; i++)
        {
            if (i < upgradeLevel)
            {
                upgradeFills[i].SetActive(true);
            }
        }
        
    }

    private bool HaveEnoughMoney()
    {
        return Credits.credits >= prices[upgradeLevel];
    }
    
    private void LoadValues()
    {
        switch (upgradeObject)
        {
            case UpgradeObject.BaseHp:
                currentValue = float.Parse(PlayerPrefs.GetString("baseHpMultiplier", "1"));
                upgradeLevel = int.Parse(PlayerPrefs.GetString("baseHpMultiplierUpgradeLevel", "0"));
                GlobalMultipliers.baseHpMultiplier = currentValue;
                break;
            case UpgradeObject.Dmg:
                currentValue = float.Parse(PlayerPrefs.GetString("dmgMultiplier", "1"));
                upgradeLevel = int.Parse(PlayerPrefs.GetString("dmgMultiplierUpgradeLevel", "0"));
                GlobalMultipliers.dmgMultiplier = currentValue;
                break;
            case UpgradeObject.ShootAngle:
                currentValue = float.Parse(PlayerPrefs.GetString("shootAngleMultiplier", "1"));
                upgradeLevel = int.Parse(PlayerPrefs.GetString("shootAngleMultiplierUpgradeLevel", "0"));
                GlobalMultipliers.shootAngleMultiplier = currentValue;
                break;
            case UpgradeObject.Money:
                currentValue = float.Parse(PlayerPrefs.GetString("moneyMultiplier", "1"));
                upgradeLevel = int.Parse(PlayerPrefs.GetString("moneyMultiplierUpgradeLevel", "0"));
                GlobalMultipliers.moneyMultiplier = currentValue;
                break;
        }
    }
}
