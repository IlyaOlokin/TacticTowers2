using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigunCoolDelay : Upgrade
{
    public override void Execute(Tower tower)
    {
        tower.gameObject.GetComponent<Minigun>().coolDelayMultiplier += actualBonus;
    }
}
