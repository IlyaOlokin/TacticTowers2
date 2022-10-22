using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxHeat : Upgrade
{
    public override void Execute(Tower tower)
    {
        tower.gameObject.GetComponent<Minigun>().maxHeat += (int) actualBonus;
    }
}
