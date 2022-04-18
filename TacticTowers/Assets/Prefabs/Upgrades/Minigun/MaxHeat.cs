using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxHeat : Upgrade
{
    [SerializeField] private int bonus;
    
    public override void Execute(Tower tower)
    {
        tower.gameObject.GetComponent<Minigun>().maxHeat += bonus;
    }
}
