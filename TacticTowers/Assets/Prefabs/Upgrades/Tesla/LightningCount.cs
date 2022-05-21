using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningCount : Upgrade
{
    [SerializeField] private int bonus;
    
    public override void Execute(Tower tower)
    {
        tower.transform.GetComponent<Tesla>().lightningCount += bonus;
    }
}

