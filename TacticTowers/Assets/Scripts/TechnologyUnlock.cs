using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TechnologyUnlock : MonoBehaviour
{
    [SerializeField] private MinUpgradePriceFinder minUpgradePriceFinder;
    
    [SerializeField] private GameObject unlockFill;
    public int price;
    [SerializeField] private Text priceText;
    [SerializeField] private GameObject button;
    [SerializeField] private Sprite enoughMoneyButton;
    [SerializeField] private Sprite notEnoughMoneyButton;
    
    [SerializeField] private UnlockableTowers unlockableTower;
    [NonSerialized] public bool isUnlocked;

    enum UnlockableTowers
    {
        Frostgun,
        Flamethrower,
        Railgun,
        Tesla
    }

    private void Start()
    {
        LoadValues();
        
        SetUpgradeVisuals(isUnlocked);
        UpdateTexts();
    }

    public void Unlock()
    {
        if (isUnlocked) return;
        if (!HaveEnoughMoney()) return;
        
        Credits.TakeCredits(price);
        isUnlocked = true;
        switch (unlockableTower)
        {
            case UnlockableTowers.Frostgun:
                Technologies.IsFrostGunUnlocked = true;
                PlayerPrefs.SetInt("isFrostGunUnlocked", Convert.ToInt16(isUnlocked));
                break;
            case UnlockableTowers.Flamethrower :
                Technologies.IsFlamethrowerUnlocked = true;
                PlayerPrefs.SetInt("isFlamethrowerUnlocked",  Convert.ToInt16(isUnlocked));
                break;
            case UnlockableTowers.Railgun :
                Technologies.IsRailgunUnlocked = true;
                PlayerPrefs.SetInt("isRailgunUnlocked",  Convert.ToInt16(isUnlocked));
                break;
            case UnlockableTowers.Tesla :
                Technologies.IsTeslaUnlocked = true;
                PlayerPrefs.SetInt("isTeslaUnlocked",  Convert.ToInt16(isUnlocked));
                break;
        }
        minUpgradePriceFinder.FindMinPrice();
        SetUpgradeVisuals(isUnlocked);
        AudioManager.Instance.Play("ButtonClick2");
    }
    
    private void UpdateTexts()
    {
        priceText.text = isUnlocked ? "Unlocked!" : price.ToString();
    }

    private void SetUpgradeVisuals(bool unlocked)
    {
        UpdateTexts();
        unlockFill.SetActive(unlocked);
        
    }
    
    private bool HaveEnoughMoney()
    {
        return Credits.credits >= price;
    }
    
    private void LoadValues()
    {
        switch (unlockableTower)
        {
            case UnlockableTowers.Frostgun:
                isUnlocked = Technologies.IsFrostGunUnlocked;
                break;
            case UnlockableTowers.Flamethrower :
                isUnlocked = Technologies.IsFlamethrowerUnlocked;
                break;
            case UnlockableTowers.Railgun :
                isUnlocked = Technologies.IsRailgunUnlocked;
                break;
            case UnlockableTowers.Tesla :
                isUnlocked = Technologies.IsTeslaUnlocked;
                break;
        }
    }
    
    private void Update()
    {
        if (!HaveEnoughMoney() || isUnlocked)
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
