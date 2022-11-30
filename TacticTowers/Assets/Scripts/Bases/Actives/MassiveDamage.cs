using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MassiveDamage : BaseActive
{
    [SerializeField] private MassiveDamageBox box;
    [SerializeField] private float damage;

    public override void ExecuteActiveAbility()
    {
        box.DamageEnemy(damage);
        GetComponent<Base>().UpdateAbilityTimer();
    }
}

