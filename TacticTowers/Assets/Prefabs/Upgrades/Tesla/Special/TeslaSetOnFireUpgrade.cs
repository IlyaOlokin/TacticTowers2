using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeslaSetOnFireUpgrade : SpecialUpgrade
{
    public override void Execute(Tower tower)
    {
        tower.transform.GetComponent<Tesla>().hasSetOnFireUpgrade = true;
        isUpgraded = true;
    }
}
