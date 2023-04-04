using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamethrowerWidthUpgrade : SpecialUpgrade
{
public override void Execute(Tower tower)
    {
        base.Execute(tower);
        tower.gameObject.GetComponent<Flamethrower>().hasWidthUpgrade = true;
    }
}

