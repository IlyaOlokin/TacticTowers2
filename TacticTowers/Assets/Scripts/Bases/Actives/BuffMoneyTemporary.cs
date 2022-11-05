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
        GlobalBaseEffects.TempMoneyMultiplier = MoneyMultiplier;
        GameObject.FindGameObjectWithTag("Base").GetComponent<Base>().UpdateAbilityTimer();
    }

    private void GoBackToMoneyMultiplier()
    {
        GlobalBaseEffects.TempMoneyMultiplier = 1;
    }
}
