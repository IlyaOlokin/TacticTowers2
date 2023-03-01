using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootDistance : Upgrade
{
    public override void Execute(Tower tower)
    {
        tower.multiplierShootDistance += actualBonus;
        tower.shootZone.DrawShootZone();
    }
}
