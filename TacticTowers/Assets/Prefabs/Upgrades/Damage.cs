using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : Upgrade
{
    [SerializeField] private int bonus;
    
    public override void Execute(Tower tower)
    {
        tower.Dmg += bonus;
    }
}
