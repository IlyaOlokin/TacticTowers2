using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserMaxHeat : CommonUpgrade
{
    public override void Execute(Tower tower)
    {
        tower.gameObject.GetComponent<Laser>().maxHeatMultiplier += actualBonus;
    }
}
