using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnDamage : CommonUpgrade
{
    public override void Execute(Tower tower)
    {
        tower.transform.GetComponent<Flamethrower>().burnDmgMultiplier += actualBonus;
    }
}