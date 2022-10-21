using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeToFreeze  : Upgrade
{
    public override void Execute(Tower tower)
    {
        tower.transform.GetComponent<Frostgun>().freezeStacksPerHitMultiplier += actualBonus;
    }
}
