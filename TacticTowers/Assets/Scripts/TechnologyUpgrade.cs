using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TechnologyUpgrade : MonoBehaviour
{
    [SerializeField] private MinUpgradePriceFinder minUpgradePriceFinder;
    
    [SerializeField] private List<GameObject> upgradeFills;
    [SerializeField] private Text valueText;
    [SerializeField] private Text priceText;
    [SerializeField] private GameObject button;

    [SerializeField] private Sprite enoughMoneyButton;
    [SerializeField] private Sprite notEnoughMoneyButton;

    public List<int> prices;
    [SerializeField] private float currentValue;
    [SerializeField] private float bonusValue;
    [SerializeField] private UpgradeObject upgradeObject;
    [NonSerialized] public int upgradeLevel = 0;
    
    enum UpgradeObject
    {
        BaseHp,
        Dmg,
        AttackSpeed,
        ShootAngle,
        ShootDistance,
        X2UpgradeChance
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
                DataLoader.SaveString("baseHpMultiplier", currentValue.ToString());
                DataLoader.SaveString("baseHpMultiplierUpgradeLevel", upgradeLevel.ToString());
                break;
            
            case UpgradeObject.Dmg:
                Technologies.DmgMultiplier = currentValue;
                DataLoader.SaveString("dmgMultiplier", currentValue.ToString());
                DataLoader.SaveString("dmgMultiplierUpgradeLevel", upgradeLevel.ToString());
                break;
            
            case UpgradeObject.AttackSpeed:
                Technologies.AttackSpeedMultiplier = currentValue;
                DataLoader.SaveString("attackSpeedMultiplier", currentValue.ToString());
                DataLoader.SaveString("attackSpeedMultiplierUpgradeLevel", upgradeLevel.ToString());
                break;
            
            case UpgradeObject.ShootAngle:
                Technologies.ShootAngleMultiplier = currentValue;
                DataLoader.SaveString("shootAngleMultiplier", currentValue.ToString());
                DataLoader.SaveString("shootAngleMultiplierUpgradeLevel", upgradeLevel.ToString());
                break;
            
            case UpgradeObject.ShootDistance:
                Technologies.ShootDistanceMultiplier = currentValue;
                DataLoader.SaveString("shootDistanceMultiplier", currentValue.ToString());
                DataLoader.SaveString("shootDistanceMultiplierUpgradeLevel", upgradeLevel.ToString());
                break;
            case UpgradeObject.X2UpgradeChance:
                Technologies.X2UpgradeChanceMultiplier = currentValue;
                DataLoader.SaveString("x2UpgradeChanceMultiplier", currentValue.ToString());
                DataLoader.SaveString("x2UpgradeChanceMultiplierUpgradeLevel", upgradeLevel.ToString());
                break;
                
        }
        minUpgradePriceFinder.FindMinPrice();
        AudioManager.Instance.Play("ButtonClick2");

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
                currentValue = float.Parse(DataLoader.LoadString("baseHpMultiplier", "1"));
                upgradeLevel = int.Parse(DataLoader.LoadString("baseHpMultiplierUpgradeLevel", "0"));
                break;
            case UpgradeObject.Dmg:
                currentValue = float.Parse(DataLoader.LoadString("dmgMultiplier", "1"));
                upgradeLevel = int.Parse(DataLoader.LoadString("dmgMultiplierUpgradeLevel", "0"));
                break;
            case UpgradeObject.AttackSpeed:
                currentValue = float.Parse(DataLoader.LoadString("attackSpeedMultiplier", "1"));
                upgradeLevel = int.Parse(DataLoader.LoadString("attackSpeedMultiplierUpgradeLevel", "0"));
                break;
            case UpgradeObject.ShootAngle:
                currentValue = float.Parse(DataLoader.LoadString("shootAngleMultiplier", "1"));
                upgradeLevel = int.Parse(DataLoader.LoadString("shootAngleMultiplierUpgradeLevel", "0"));
                break;
            case UpgradeObject.ShootDistance:
                currentValue = float.Parse(DataLoader.LoadString("shootDistanceMultiplier", "1"));
                upgradeLevel = int.Parse(DataLoader.LoadString("shootDistanceMultiplierUpgradeLevel", "0"));
                break;
            case UpgradeObject.X2UpgradeChance:
                currentValue = float.Parse(DataLoader.LoadString("x2UpgradeChanceMultiplier", "1"));
                upgradeLevel = int.Parse(DataLoader.LoadString("x2UpgradeChanceMultiplierUpgradeLevel", "0"));
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
