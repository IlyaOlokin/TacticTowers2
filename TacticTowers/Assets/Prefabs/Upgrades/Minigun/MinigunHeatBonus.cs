using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigunHeatBonus : Upgrade
{
    public override void Execute(Tower tower)
    {
        tower.gameObject.GetComponent<Minigun>().bonusAttackSpeedPerHeat += actualBonus;
    }
}
