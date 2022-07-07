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
        
        Technologies.IsFrostGunUnlocked = Convert.ToBoolean(PlayerPrefs.GetInt("isFrostGunUnlocked", 0));
        Technologies.IsFlamethrowerUnlocked = Convert.ToBoolean(PlayerPrefs.GetInt("isFlamethrowerUnlocked", 0));
        Technologies.IsRailgunUnlocked = Convert.ToBoolean(PlayerPrefs.GetInt("isRailgunUnlocked", 0));
        Technologies.IsTeslaUnlocked = Convert.ToBoolean(PlayerPrefs.GetInt("isTeslaUnlocked", 0));

        Credits.credits = int.Parse(PlayerPrefs.GetString("Credits", "0"));
        Credits.CreditsInTotal = int.Parse(PlayerPrefs.GetString("CreditsInTotal",  Credits.credits.ToString()));

        Technologies.MinUpgradePrice = PlayerPrefs.GetInt("minUpgradePrice", 10);
        
        YandexSDK.Instance.Authenticate();
    }

    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.N))
        {
            Credits.AddCredits(100);
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            PlayerPrefs.DeleteAll();
        }*/
    }
}
