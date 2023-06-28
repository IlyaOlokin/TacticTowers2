using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Technologies
{
    public static float BaseHpMultiplier = 1;
    public static float DmgMultiplier = 1;
    public static float AttackSpeedMultiplier = 1;
    public static float ShootAngleMultiplier = 1;
    public static float ShootDistanceMultiplier = 1;
    public static float X2UpgradeChanceMultiplier = 1;

    public static bool IsFrostGunUnlocked = false;
    public static bool IsFlamethrowerUnlocked = false;
    public static bool IsRailgunUnlocked = false;
    public static bool IsTeslaUnlocked = false;

    public static int MinUpgradePrice = 10;

    public static bool TryChangeMinUpgradePrice(int price)
    {
        if (price > MinUpgradePrice)
        {
            MinUpgradePrice = price;
            return true;
        }

        return false;
    }

    public static void LoadData()
    {
        BaseHpMultiplier = float.Parse(DataLoader.LoadString("baseHpMultiplier", "1"));
        DmgMultiplier = float.Parse(DataLoader.LoadString("dmgMultiplier", "1"));
        AttackSpeedMultiplier = float.Parse(DataLoader.LoadString("attackSpeedMultiplier", "1"));
        ShootAngleMultiplier = float.Parse(DataLoader.LoadString("shootAngleMultiplier", "1"));
        ShootDistanceMultiplier = float.Parse(DataLoader.LoadString("shootDistanceMultiplier", "1"));
        X2UpgradeChanceMultiplier = float.Parse(DataLoader.LoadString("x2UpgradeChanceMultiplier", "1"));

        IsFrostGunUnlocked = Convert.ToBoolean(DataLoader.LoadInt("isFrostGunUnlocked", 0));
        IsFlamethrowerUnlocked = Convert.ToBoolean(DataLoader.LoadInt("isFlamethrowerUnlocked", 0));
        IsRailgunUnlocked = Convert.ToBoolean(DataLoader.LoadInt("isRailgunUnlocked", 0));
        IsTeslaUnlocked = Convert.ToBoolean(DataLoader.LoadInt("isTeslaUnlocked", 0));
    }
}
