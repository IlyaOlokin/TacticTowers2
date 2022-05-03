using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailGunMinDmg : Upgrade
{
    [SerializeField] private float bonus;

    public override void Execute(Tower tower)
    {
        tower.transform.GetComponent<Railgun>().minDmg += bonus;
    }
}

