using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostgunWidthUpgrade : SpecialUpgrade
{
    public override void Execute(Tower tower)
    {
        base.Execute(tower);
        tower.gameObject.GetComponent<Frostgun>().hasWidthUpgrade = true;
    }
}