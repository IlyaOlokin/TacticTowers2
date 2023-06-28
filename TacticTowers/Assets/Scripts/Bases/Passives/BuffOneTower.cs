using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class BuffOneTower : BasePassive
{
    [SerializeField] private float ReductionDmgMultiplier;
    [SerializeField] private float ReductionAttackSpeedMultiplier;
    [SerializeField] private float ReductionShootAngleMultiplier;
    [SerializeField] private float ReductionShootDistanceMultiplier;

    [SerializeField] private float IncreaseDmgMultiplier;
    [SerializeField] private float IncreaseAttackSpeedMultiplier;
    [SerializeField] private float IncreaseShootAngleMultiplier;
    [SerializeField] private float IncreaseShootDistanceMultiplier;

    public override void ExecutePassiveEffect()
    {
        GlobalBaseEffects.ApplyToAllTowers(ReductionDmgMultiplier, ReductionAttackSpeedMultiplier,
            ReductionShootAngleMultiplier, ReductionShootDistanceMultiplier);

        var rnd = new Random();
        var value = rnd.Next(1, 5);

        switch (value)
        {
            case 1:
                GlobalBaseEffects.ApplyToUpTowers(IncreaseDmgMultiplier, IncreaseAttackSpeedMultiplier,
                    IncreaseShootAngleMultiplier, IncreaseShootDistanceMultiplier);
                break;
            case 2:
                GlobalBaseEffects.ApplyToDownTowers(IncreaseDmgMultiplier, IncreaseAttackSpeedMultiplier,
                    IncreaseShootAngleMultiplier, IncreaseShootDistanceMultiplier);
                break;
            case 3:
                GlobalBaseEffects.ApplyToLeftTowers(IncreaseDmgMultiplier, IncreaseAttackSpeedMultiplier,
                    IncreaseShootAngleMultiplier, IncreaseShootDistanceMultiplier);
                break;
            case 4:
                GlobalBaseEffects.ApplyToRightTowers(IncreaseDmgMultiplier, IncreaseAttackSpeedMultiplier,
                    IncreaseShootAngleMultiplier, IncreaseShootDistanceMultiplier);
                break;
        }
    }
}
