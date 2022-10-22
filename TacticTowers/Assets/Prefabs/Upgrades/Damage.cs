using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : Upgrade
{
    public override void Execute(Tower tower)
    {
        tower.multiplierDmg += actualBonus;
    }
}
