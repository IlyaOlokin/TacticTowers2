using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialUpgrade : Upgrade
{
    [SerializeField] protected int upgradeIndex;
    
    public override void Execute(Tower tower)
    {
        tower.transform.GetComponent<Tower>().upgradedSpecilaUpgrades[upgradeIndex] = true;
        tower.UpgradeVisual();
    }
}
