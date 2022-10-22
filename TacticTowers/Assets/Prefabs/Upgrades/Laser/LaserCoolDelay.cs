using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCoolDelay : Upgrade
{
    public override void Execute(Tower tower)
    {
        tower.gameObject.GetComponent<Laser>().coolDelay += actualBonus;
    }
}
