using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeslaBranchingUpgrade : SpecialUpgrade
{
    public override void Execute(Tower tower)
    {
        tower.transform.GetComponent<Tesla>().hasBranchingUpgrade = true;
        isUpgraded = true;
    }
}
