using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarExplosionRadius : CommonUpgrade
{
    public override void Execute(Tower tower)
    {
        tower.GetComponent<Mortar>().explosionRadiusMultiplier += actualBonus;
    }
}
