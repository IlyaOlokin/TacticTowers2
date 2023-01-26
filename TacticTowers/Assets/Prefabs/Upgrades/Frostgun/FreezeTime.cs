using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeTime : Upgrade
{
    public override void Execute(Tower tower)
    {
        tower.transform.GetComponent<Frostgun>().freezeTimeMultiplier += actualBonus;
    }
}