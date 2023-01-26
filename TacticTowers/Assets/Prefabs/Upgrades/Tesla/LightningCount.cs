using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningCount : Upgrade
{
    public override void Execute(Tower tower)
    {
        tower.transform.GetComponent<Tesla>().bonusLightningCount += (int) actualBonus;
    }
}