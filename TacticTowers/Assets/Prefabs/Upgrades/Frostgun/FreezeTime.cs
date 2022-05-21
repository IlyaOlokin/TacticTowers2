using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeTime : Upgrade
{
    [SerializeField] private float bonus;

    public override void Execute(Tower tower)
    {
        tower.transform.GetComponent<Frostgun>().freezeTime += bonus;
    }
}