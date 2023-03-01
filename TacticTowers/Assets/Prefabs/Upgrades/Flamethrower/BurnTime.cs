using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnTime : CommonUpgrade
{
    public override void Execute(Tower tower)
    {
        tower.transform.GetComponent<Flamethrower>().burnTimeMultiplier += actualBonus;
    }
}
