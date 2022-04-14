using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigunHeatBonus : Upgrade
{
    [SerializeField] private float bonus;
    
    public override void Execute(Tower tower)
    {
        tower.gameObject.GetComponent<Minigun>().bonusAttackSpeedPerHeat += bonus;
    }
}
