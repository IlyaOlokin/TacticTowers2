using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserHeatBonus : CommonUpgrade
{
    public override void Execute(Tower tower)
    {
        tower.gameObject.GetComponent<Laser>().multiplierPerHeatStackMultiplier += actualBonus;
    }
}

