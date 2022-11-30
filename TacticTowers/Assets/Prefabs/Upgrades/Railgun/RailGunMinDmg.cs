using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailGunMinDmg : Upgrade
{
    public override void Execute(Tower tower)
    {
        tower.transform.GetComponent<Railgun>().minDmgMultiplier += actualBonus;
    }
}

