using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TechnologyUnlock : MonoBehaviour
{
    [SerializeField] private GameObject unlockFill;
    [SerializeField] private int price;
    [SerializeField] private Text priceText;
    [SerializeField] private GameObject button;
    [SerializeField] private Sprite enoughMoneyButton;
    [SerializeField] private Sprite notEnoughMoneyButton;
    
    [SerializeField] private UnlockableTowers unlockableTower;
    private bool isUnlocked;

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
                PlayerPrefs.SetString("isFrostGunUnlocked", isUnlocked.ToString());
                break;
            case UnlockableTowers.Flamethrower :
                Technologies.IsFlamethrowerUnlocked = true;
                PlayerPrefs.SetString("isFlamethrowerUnlocked", isUnlocked.ToString());
                break;
            case UnlockableTowers.Railgun :
                Technologies.IsRailgunUnlocked = true;
                PlayerPrefs.SetString("isRailgunUnlocked", isUnlocked.ToString());
                break;
            case UnlockableTowers.Tesla :
                Technologies.IsTeslaUnlocked = true;
                PlayerPrefs.SetString("isTeslaUnlocked", isUnlocked.ToString());
                break;
        }
        
        SetUpgradeVisuals(isUnlocked);
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
