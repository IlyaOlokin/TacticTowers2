using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootDelay : Upgrade
{
    [SerializeField] private float bonus;
    
    public override void Execute(Tower tower)
    {
        tower.attackSpeed += bonus;
    }
}
