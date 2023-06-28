using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamethrowerCloseDamageUpgrade : SpecialUpgrade
{
    public override void Execute(Tower tower)
    {
        base.Execute(tower);
        tower.gameObject.GetComponent<Flamethrower>().hasCloseDamageUpgrade = true;
    }
}

