using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunKnockBackUpgrade : SpecialUpgrade
{
    public override void Execute(Tower tower)
    {
        base.Execute(tower);
        tower.gameObject.GetComponent<Shotgun>().hasKnockBackUpgrade = true;
    }
}
