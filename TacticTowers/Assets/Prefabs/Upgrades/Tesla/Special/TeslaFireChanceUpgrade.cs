using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeslaFireChanceUpgrade : SpecialUpgrade
{
    public override void Execute(Tower tower)
    {
        base.Execute(tower);
        tower.transform.GetComponent<Tesla>().hasFireChanceUpgrade = true;
    }
}
