using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffMoneyTemporary : BaseActive
{
    [SerializeField] private float MoneyMultiplier;
    [SerializeField] private float duration;
    public override void ExecuteActiveAbility()
    {
        FunctionTimer.Create(GoBackToMoneyMultiplier, duration);
        GlobalBaseEffects.TempMoneyMultiplier *= MoneyMultiplier;
    }

    private void GoBackToMoneyMultiplier()
    {
        GlobalBaseEffects.TempMoneyMultiplier /= MoneyMultiplier;
    }
}
