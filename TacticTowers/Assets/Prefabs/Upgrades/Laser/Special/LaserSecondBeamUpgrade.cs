using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSecondBeamUpgrade : SpecialUpgrade
{
    public override void Execute(Tower tower)
    {
        base.Execute(tower);
        tower.transform.GetComponent<Laser>().hasSecondBeamUpgrade = true;
    }
}
