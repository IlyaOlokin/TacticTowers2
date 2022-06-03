using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnTime : Upgrade
{
    [SerializeField] private float bonus;

    public override void Execute(Tower tower)
    {
        tower.transform.GetComponent<Flamethrower>().burnTime += bonus;
    }
}
