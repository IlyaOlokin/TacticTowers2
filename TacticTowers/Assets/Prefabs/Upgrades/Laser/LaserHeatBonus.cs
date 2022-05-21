using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserHeatBonus : Upgrade
{
    [SerializeField] private float bonus;
    
    public override void Execute(Tower tower)
    {
        tower.gameObject.GetComponent<Laser>().bonusDamagePerHeat += bonus;
    }
}

