using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDmgDescrease : Upgrade
{
    public override void Execute(Tower tower)
    {
        tower.transform.GetComponent<Tesla>().dmgDecrease += actualBonus;
    }
}