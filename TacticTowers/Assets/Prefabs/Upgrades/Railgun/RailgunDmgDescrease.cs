using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailgunDmgDescrease : Upgrade
{
    [SerializeField] private float bonus;

    public override void Execute(Tower tower)
    {
        tower.transform.GetComponent<Railgun>().dmgMultiplier += bonus;
    }
}
