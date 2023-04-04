using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeTime : CommonUpgrade
{
    public override void Execute(Tower tower)
    {
        tower.transform.GetComponent<Frostgun>().freezeTimeMultiplier += actualBonus;
    }
}