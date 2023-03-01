using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigunCoolDelay : CommonUpgrade
{
    public override void Execute(Tower tower)
    {
        tower.gameObject.GetComponent<Minigun>().coolDelayMultiplier += actualBonus;
    }
}
