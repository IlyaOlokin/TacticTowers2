using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningJumpDistance : Upgrade
{
    public override void Execute(Tower tower)
    {
        tower.transform.GetComponent<Tesla>().lightningJumpDistanceMultiplier += actualBonus;
    }
}