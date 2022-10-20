using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class BuffOneTowerTemporary : BaseActive
{
    [SerializeField] private float dmgMultiplier;
    [SerializeField] private float attackSpeedMultiplier;
    [SerializeField] private float shootAngleMultiplier;
    [SerializeField] private float shootDistanceMultiplier;

    [SerializeField] private float duration;

    public override void ExecuteActiveAbility()
    {
        var rnd = new Random();
        var value = rnd.Next(1, 5);

        switch (value)
        {
            case 1:
                GlobalBaseEffects.ApplyToUpTowersTemporary(dmgMultiplier, attackSpeedMultiplier,
                    shootAngleMultiplier, shootDistanceMultiplier, duration);
                break;
            case 2:
                GlobalBaseEffects.ApplyToDownTowersTemporary(dmgMultiplier, attackSpeedMultiplier,
                    shootAngleMultiplier, shootDistanceMultiplier, duration);
                break;
            case 3:
                GlobalBaseEffects.ApplyToLeftTowersTemporary(dmgMultiplier, attackSpeedMultiplier,
                    shootAngleMultiplier, shootDistanceMultiplier, duration);
                break;
            case 4:
                GlobalBaseEffects.ApplyToRightTowersTemporary(dmgMultiplier, attackSpeedMultiplier,
                    shootAngleMultiplier, shootDistanceMultiplier, duration);
                break;
        }
    }
}
