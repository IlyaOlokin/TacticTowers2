using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : Upgrade
{
    [SerializeField] private float bonus;
    
    public override void Execute(Tower tower)
    {
        tower.multiplierDmg += bonus;
    }
}
