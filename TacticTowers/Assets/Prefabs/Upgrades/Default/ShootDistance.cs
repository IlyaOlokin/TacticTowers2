using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootDistance : CommonUpgrade
{
    public override void Execute(Tower tower)
    {
        tower.multiplierShootDistance += actualBonus;
        tower.shootZone.DrawShootZone();
    }
}
