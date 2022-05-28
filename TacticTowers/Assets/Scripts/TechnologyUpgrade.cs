using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TechnologyUpgrade : MonoBehaviour
{
    [SerializeField] private List<GameObject> upgradeFills;
    [SerializeField] private Text valueText;
    [SerializeField] private Text priceText;
    [SerializeField] private GameObject button;

    [SerializeField] private Sprite enoughMoneyButton;
    [SerializeField] private Sprite notEnoughMoneyButton;

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

    public void Upgrade()
    {
        Credits.TakeCredits(prices[upgradeLevel]);
        currentValue += bonusValue;
        upgradeLevel++;
        SetUpgradeVisuals(upgradeLevel);

        switch (upgradeObject)
        {
            case UpgradeObject.BaseHp:
                Technologies.BaseHpMultiplier = currentValue;
                PlayerPrefs.SetString("baseHpMultiplier", currentValue.ToString());
                PlayerPrefs.SetString("baseHpMultiplierUpgradeLevel", upgradeLevel.ToString());
                break;
            case UpgradeObject.Dmg :
                Technologies.DmgMultiplier = currentValue;
                PlayerPrefs.SetString("dmgMultiplier", currentValue.ToString());
                PlayerPrefs.SetString("dmgMultiplierUpgradeLevel", upgradeLevel.ToString());

                break;
            case UpgradeObject.ShootAngle :
                Technologies.ShootAngleMultiplier = currentValue;
                PlayerPrefs.SetString("shootAngleMultiplier", currentValue.ToString());
                PlayerPrefs.SetString("shootAngleMultiplierUpgradeLevel", upgradeLevel.ToString());

                break;
            case UpgradeObject.Money :
                Technologies.MoneyMultiplier = currentValue;
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
                break;
            case UpgradeObject.Dmg:
                currentValue = float.Parse(PlayerPrefs.GetString("dmgMultiplier", "1"));
                upgradeLevel = int.Parse(PlayerPrefs.GetString("dmgMultiplierUpgradeLevel", "0"));
                break;
            case UpgradeObject.ShootAngle:
                currentValue = float.Parse(PlayerPrefs.GetString("shootAngleMultiplier", "1"));
                upgradeLevel = int.Parse(PlayerPrefs.GetString("shootAngleMultiplierUpgradeLevel", "0"));
                break;
            case UpgradeObject.Money:
                currentValue = float.Parse(PlayerPrefs.GetString("moneyMultiplier", "1"));
                upgradeLevel = int.Parse(PlayerPrefs.GetString("moneyMultiplierUpgradeLevel", "0"));
                break;
        }
    }

    private void Update()
    {
        if (upgradeLevel == prices.Count || !HaveEnoughMoney())
        {
            button.GetComponent<Image>().sprite = notEnoughMoneyButton;
            button.GetComponent<Button>().enabled = false;
        }
        else
        {
            button.GetComponent<Image>().sprite = enoughMoneyButton;
            button.GetComponent<Button>().enabled = true;
        }
    }
}
