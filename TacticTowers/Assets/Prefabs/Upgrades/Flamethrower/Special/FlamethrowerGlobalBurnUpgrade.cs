using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamethrowerGlobalBurnUpgrade : SpecialUpgrade
{
    public override void Execute(Tower tower)
    {
        base.Execute(tower);
        tower.gameObject.GetComponent<Flamethrower>().hasGlobalBurnUpgrade = true;
        Fire.MultiplyGlobalBurnMultiplier(1.5f);
    }
}
