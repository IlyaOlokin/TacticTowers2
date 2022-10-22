using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserMaxHeat : Upgrade
{
    public override void Execute(Tower tower)
    {
        tower.gameObject.GetComponent<Laser>().maxHeat += (int) actualBonus;
    }
}
