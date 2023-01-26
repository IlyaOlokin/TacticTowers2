using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarExplosionRadius : Upgrade
{
    public override void Execute(Tower tower)
    {
        tower.GetComponent<Mortar>().explosionRadiusMultiplier += actualBonus;
    }
}
