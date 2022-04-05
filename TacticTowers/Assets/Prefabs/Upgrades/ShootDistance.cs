using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootDistance : Upgrade
{
    [SerializeField] private float bonus;
    
    public override void Execute(Tower tower)
    {
        tower.shootDistance += bonus;
        tower.shootZone.DrawShootZone();
    }
}
