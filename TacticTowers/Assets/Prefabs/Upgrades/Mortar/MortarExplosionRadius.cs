using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarExplosionRadius : Upgrade
{
    [SerializeField] private float bonus;

    public override void Execute(Tower tower)
    {
        tower.GetComponent<Mortar>().explosionRadius += bonus;
    }
}
