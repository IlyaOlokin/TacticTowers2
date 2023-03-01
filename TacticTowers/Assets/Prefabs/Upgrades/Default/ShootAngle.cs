using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAngle : CommonUpgrade
{
    public override void Execute(Tower tower)
    {
        tower.multiplierShootAngle += actualBonus;
        tower.shootZone.DrawShootZone();
    }
}
