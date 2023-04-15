using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostgunFrozenDamageUpgrade : SpecialUpgrade
{
    public override void Execute(Tower tower) 
    {
        base.Execute(tower); 
        tower.gameObject.GetComponent<Frostgun>().hasFrozenDamageUpgrade = true;
        Freeze.MultiplyGlobalFrozenMultiplier(1.25f);
        Freeze.SetActiveFrozenDamageMultiplier(true);
    }
}
