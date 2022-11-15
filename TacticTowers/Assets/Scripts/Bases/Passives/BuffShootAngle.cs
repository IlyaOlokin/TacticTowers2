using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffShootAngle : BasePassive
{
    [SerializeField] private float DmgMultiplier;
    [SerializeField] private float ShootAngleMultiplier;

    public override void ExecutePassiveEffect()
    {
        GlobalBaseEffects.ApplyToAllTowersDmgMultiplier(DmgMultiplier);
        GlobalBaseEffects.ApplyToAllTowersShootAngleMultiplier(ShootAngleMultiplier);
    }
}
