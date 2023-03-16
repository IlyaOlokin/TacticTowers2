using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigunHeatBonus : CommonUpgrade
{
    public override void Execute(Tower tower)
    {
        tower.gameObject.GetComponent<Minigun>().bonusAttackSpeedPerHeatMultiplier += actualBonus;
    }
}
