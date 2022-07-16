using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffHorizontalTowers : BasePassive
{
    [SerializeField] private float horizontalDmgMultiplier;
    [SerializeField] private float horizontalAttackSpeedMultiplier;
    [SerializeField] private float horizontalShootAngleMultiplier;
    [SerializeField] private float horizontalShootDistanceMultiplier;
    
    [SerializeField] private float verticalDmgMultiplier;
    [SerializeField] private float verticalAttackSpeedMultiplier;
    [SerializeField] private float verticalShootAngleMultiplier;
    [SerializeField] private float verticalShootDistanceMultiplier;
    
    public override void ExecutePassiveEffect()
    {
        GlobalBaseEffects.ApplyToHorizontalTowers(horizontalDmgMultiplier, horizontalAttackSpeedMultiplier,
            horizontalShootAngleMultiplier, horizontalShootDistanceMultiplier);
        GlobalBaseEffects.ApplyToVerticalTowers(verticalDmgMultiplier, verticalAttackSpeedMultiplier,
            verticalShootAngleMultiplier, verticalShootDistanceMultiplier);
    }
}
