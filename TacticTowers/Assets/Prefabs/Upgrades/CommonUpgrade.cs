using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonUpgrade : Upgrade
{
    [SerializeField] protected float bonusForFormatting;
    public Sprite UpgradeSprite;
    [SerializeField] protected float bonus;
    protected float actualBonus;

    public string FormatUpgradeText(bool superUpgrade)
    {
        return String.Format(upgradeText, superUpgrade ? bonusForFormatting * 2 : bonusForFormatting);
    }
    
    public float GetBonusForFormatting() => bonusForFormatting;
    public void ApplyBonusIncrement(bool superUpgrade)
    {
        actualBonus = bonus;
        if (superUpgrade)
            actualBonus = bonus * 2;
    }
}
