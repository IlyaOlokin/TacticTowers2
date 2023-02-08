using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigunMaxHeat : Upgrade
{
    public override void Execute(Tower tower)
    {
        tower.gameObject.GetComponent<Minigun>().maxHeatMultiplier += actualBonus;
    }
}
