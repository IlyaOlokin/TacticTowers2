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
        Technologies.MoneyMultiplier *= MoneyMultiplier;
    }

    private void GoBackToMoneyMultiplier()
    {
        Technologies.MoneyMultiplier /= MoneyMultiplier;
    }
}
