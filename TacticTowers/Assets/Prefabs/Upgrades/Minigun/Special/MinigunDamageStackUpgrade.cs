using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigunDamageStackUpgrade : SpecialUpgrade
{
    public override void Execute(Tower tower)
    {
        base.Execute(tower);
        tower.gameObject.GetComponent<Minigun>().hasDamageStackUpgrade = true;
    }
}