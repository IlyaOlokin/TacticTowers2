using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffAllTowers : BaseActive
{
    
    [SerializeField] private float dmgMultiplier;
    [SerializeField] private float attackSpeedMultiplier;
    [SerializeField] private float shootAngleMultiplier;
    [SerializeField] private float shootDistanceMultiplier;

    [SerializeField] private float duration;
    
    public override void ExecuteActiveAbility()
    {
        GlobalBaseEffects.ApplyToAllTowersTemporary(dmgMultiplier, attackSpeedMultiplier, shootAngleMultiplier,
            shootDistanceMultiplier, duration);
        GameObject.FindGameObjectWithTag("Base").GetComponent<Base>().UpdateAbilityTimer();
    }
}
