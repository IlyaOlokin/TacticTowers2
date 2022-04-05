using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAngle : Upgrade
{
    [SerializeField] private float bonus;
    
    public override void Execute(Tower tower)
    {
        tower.shootAngle += bonus;
        tower.shootZone.DrawShootZone();
    }
}
