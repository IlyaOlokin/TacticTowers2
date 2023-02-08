using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigunPenetrationUpgrade : SpecialUpgrade
{
    public override void Execute(Tower tower)
    {
        base.Execute(tower);
        tower.gameObject.GetComponent<Minigun>().hasPenetrationUpgrade = true;
    }
}
