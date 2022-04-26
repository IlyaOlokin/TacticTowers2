using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningJumpDistance : Upgrade
{
    [SerializeField] private float bonus;

    public override void Execute(Tower tower)
    {
        tower.transform.GetComponent<Tesla>().lightningJumpDistance += bonus;
    }
}