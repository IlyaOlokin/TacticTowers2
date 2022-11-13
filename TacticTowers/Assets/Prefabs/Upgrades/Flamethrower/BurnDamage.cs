using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnDamage : Upgrade
{
    public override void Execute(Tower tower)
    {
        tower.transform.GetComponent<Flamethrower>().burnDmgMultiplier += actualBonus;
    }
}