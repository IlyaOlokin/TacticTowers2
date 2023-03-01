using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailGunMinDmg : CommonUpgrade
{
    public override void Execute(Tower tower)
    {
        tower.transform.GetComponent<Railgun>().minDmgMultiplier += actualBonus;
    }
}

