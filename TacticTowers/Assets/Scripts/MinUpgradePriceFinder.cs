using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinUpgradePriceFinder : MonoBehaviour
{
    [SerializeField] private List<TechnologyUpgrade> technologyUpgrades;
    [SerializeField] private List<TechnologyUnlock> technologyUnlocks;

    public void FindMinPrice()
    {
        int minPrice = Int16.MaxValue;
        foreach (var technology in technologyUpgrades)
        {
            if (technology.upgradeLevel == technology.prices.Count) continue;
            int price = technology.prices[technology.upgradeLevel];
            if (price < minPrice)
            {
                minPrice = price;
            }
        }
        foreach (var technology in technologyUnlocks)
        {
            if (technology.isUnlocked) continue;
            int price = technology.price;
            if (price < minPrice)
            {
                minPrice = price;
            }
        }

        if (Technologies.TryChangeMinUpgradePrice(minPrice))
        {
            DataLoader.SaveInt("minUpgradePrice", minPrice);
        }
        
    }
}
