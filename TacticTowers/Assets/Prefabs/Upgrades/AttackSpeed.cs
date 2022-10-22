using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpeed : Upgrade
{
    public override void Execute(Tower tower)
    {
        tower.multiplierAttackSpeed += actualBonus;
    }
}