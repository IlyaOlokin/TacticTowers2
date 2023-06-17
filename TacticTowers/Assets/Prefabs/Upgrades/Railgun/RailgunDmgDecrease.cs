using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailgunDmgDecrease : CommonUpgrade
{
    public override void Execute(Tower tower)
    {
        tower.transform.GetComponent<Railgun>().dmgMultiplierMultiplier += actualBonus;
    }
}
