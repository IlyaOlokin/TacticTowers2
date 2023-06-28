using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunDoubleDamageUpgrade : SpecialUpgrade
{
    public override void Execute(Tower tower)
    {
        base.Execute(tower);
        tower.gameObject.GetComponent<Shotgun>().hasDoubleDamageUpgrade = true;
    }
}
