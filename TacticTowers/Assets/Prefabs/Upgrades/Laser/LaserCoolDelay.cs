using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCoolDelay : CommonUpgrade
{
    public override void Execute(Tower tower)
    {
        tower.gameObject.GetComponent<Laser>().coolDelayMultiplier += actualBonus;
    }
}
