using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLoader : MonoBehaviour
{
    void Start()
    {
        YandexSDK.Instance.GettingData();
    }

    public static void LoadStartData()
    {
        Technologies.BaseHpMultiplier = float.Parse(LoadString("baseHpMultiplier", "1"));
        Technologies.DmgMultiplier = float.Parse(LoadString("dmgMultiplier", "1"));
        Technologies.AttackSpeedMultiplier = float.Parse(LoadString("attackSpeedMultiplier", "1"));
        Technologies.ShootAngleMultiplier = float.Parse(LoadString("shootAngleMultiplier", "1"));
        Technologies.ShootDistanceMultiplier = float.Parse(LoadString("shootDistanceMultiplier", "1"));

        Technologies.IsFrostGunUnlocked = Convert.ToBoolean(LoadInt("isFrostGunUnlocked", 0));
        Technologies.IsFlamethrowerUnlocked = Convert.ToBoolean(LoadInt("isFlamethrowerUnlocked", 0));
        Technologies.IsRailgunUnlocked = Convert.ToBoolean(LoadInt("isRailgunUnlocked", 0));
        Technologies.IsTeslaUnlocked = Convert.ToBoolean(LoadInt("isTeslaUnlocked", 0));

        Credits.credits = int.Parse(LoadString("Credits", "0"));
        Credits.CreditsInTotal = int.Parse(LoadString("CreditsInTotal", Credits.credits.ToString()));

        Technologies.MinUpgradePrice = LoadInt("minUpgradePrice", 10);

        BaseSelectManager.SelectedBaseIndex = LoadInt("selectedBaseIndex", 0);

        Localisation.CurrentLanguage = (Language) LoadInt("currentLanguage", 0);
        Localisation.OnLanguageChanged.Invoke();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            Credits.AddCredits(1000);
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            PlayerPrefs.DeleteAll();
        }
    }

    public static void SaveInt(string variableName, int isUnlocked)
    {
        if (!YandexSDK.Instance.playerData.intData.ContainsKey(variableName))
            YandexSDK.Instance.playerData.intData.Add(variableName, isUnlocked);
        else
            YandexSDK.Instance.playerData.intData[variableName] = isUnlocked;
        
        YandexSDK.Instance.SettingData();
    }

    public static void SaveInt(string variableName, bool isUnlocked)
    {
        if (!YandexSDK.Instance.playerData.intData.ContainsKey(variableName))
            YandexSDK.Instance.playerData.intData.Add(variableName, Convert.ToInt16(isUnlocked));
        else
            YandexSDK.Instance.playerData.intData[variableName] = Convert.ToInt16(isUnlocked);
        YandexSDK.Instance.SettingData();
    }

    public static void SaveString(string variableName, string value)
    {
        PlayerPrefs.SetString(variableName, value);
        if (!YandexSDK.Instance.playerData.stringData.ContainsKey(variableName))
            YandexSDK.Instance.playerData.stringData.Add(variableName, value);
        else
            YandexSDK.Instance.playerData.stringData[variableName] = value;
        YandexSDK.Instance.SettingData();
    }

    public static string LoadString(string variableName, string defaultValue)
    {
        if (!YandexSDK.Instance.playerData.stringData.ContainsKey(variableName))
            return defaultValue;
       
        return YandexSDK.Instance.playerData.stringData[variableName];
    }
    
    public static int LoadInt(string variableName, int defaultValue)
    {
        if (!YandexSDK.Instance.playerData.intData.ContainsKey(variableName))
            return defaultValue;
       
        return YandexSDK.Instance.playerData.intData[variableName];
    }
}
