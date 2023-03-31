using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarFireChanceUpgrade : SpecialUpgrade
{
    public override void Execute(Tower tower)
    {
        base.Execute(tower);
        tower.transform.GetComponent<Mortar>().hasFireChanceUpgrade = true;
    }
}