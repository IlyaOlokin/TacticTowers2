using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigunCoolDelay : Upgrade
{
    [SerializeField] private float bonus;
    
    public override void Execute(Tower tower)
    {
        tower.gameObject.GetComponent<Minigun>().coolDelay += bonus;
    }
}
