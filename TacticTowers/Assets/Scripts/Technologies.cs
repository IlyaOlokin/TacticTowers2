using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Technologies
{
    public static float BaseHpMultiplier = 1;
    public static float DmgMultiplier = 1;
    public static float ShootAngleMultiplier = 1;
    public static float MoneyMultiplier = 1;

    public static bool IsFrostGunUnlocked = false;
    public static bool IsFlamethrowerUnlocked = false;
    public static bool IsRailgunUnlocked = false;
    public static bool IsTeslaUnlocked = false;

    public static int MinUpgradePrice = 10;

    public static bool TryChangeMinUpgradePrice(int price)
    {
        if (price < MinUpgradePrice)
        {
            MinUpgradePrice = price;
            return true;
        }

        return false;
    }
}
