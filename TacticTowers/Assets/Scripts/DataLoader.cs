using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLoader : MonoBehaviour
{

    void Start()
    {
        GlobalUpgrades.BaseHpMultiplier = float.Parse(PlayerPrefs.GetString("baseHpMultiplier", "1"));
        GlobalUpgrades.DmgMultiplier = float.Parse(PlayerPrefs.GetString("dmgMultiplier", "1"));
        GlobalUpgrades.ShootAngleMultiplier = float.Parse(PlayerPrefs.GetString("shootAngleMultiplier", "1"));
        GlobalUpgrades.MoneyMultiplier = float.Parse(PlayerPrefs.GetString("moneyMultiplier", "1"));
        
        GlobalUpgrades.IsFrostGunUnlocked = bool.Parse(PlayerPrefs.GetString("isFrostGunUnlocked", "false"));
        GlobalUpgrades.IsFlamethrowerUnlocked = bool.Parse(PlayerPrefs.GetString("isFlamethrowerUnlocked", "false"));
        GlobalUpgrades.IsRailgunUnlocked = bool.Parse(PlayerPrefs.GetString("isRailgunUnlocked", "false"));
        GlobalUpgrades.IsTeslaUnlocked = bool.Parse(PlayerPrefs.GetString("isTeslaUnlocked", "false"));

        Credits.credits = int.Parse(PlayerPrefs.GetString("Credits", "0"));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            Credits.AddCredits(100);
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            PlayerPrefs.DeleteAll();
        }
    }
}
