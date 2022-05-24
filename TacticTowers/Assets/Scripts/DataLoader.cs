using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLoader : MonoBehaviour
{

    void Start()
    {
        Technologies.BaseHpMultiplier = float.Parse(PlayerPrefs.GetString("baseHpMultiplier", "1"));
        Technologies.DmgMultiplier = float.Parse(PlayerPrefs.GetString("dmgMultiplier", "1"));
        Technologies.ShootAngleMultiplier = float.Parse(PlayerPrefs.GetString("shootAngleMultiplier", "1"));
        Technologies.MoneyMultiplier = float.Parse(PlayerPrefs.GetString("moneyMultiplier", "1"));
        
        Technologies.IsFrostGunUnlocked = bool.Parse(PlayerPrefs.GetString("isFrostGunUnlocked", "false"));
        Technologies.IsFlamethrowerUnlocked = bool.Parse(PlayerPrefs.GetString("isFlamethrowerUnlocked", "false"));
        Technologies.IsRailgunUnlocked = bool.Parse(PlayerPrefs.GetString("isRailgunUnlocked", "false"));
        Technologies.IsTeslaUnlocked = bool.Parse(PlayerPrefs.GetString("isTeslaUnlocked", "false"));

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
