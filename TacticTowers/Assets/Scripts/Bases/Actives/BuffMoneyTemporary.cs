using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffMoneyTemporary : BaseActive
{
    [SerializeField] private float MoneyMultiplier;
    [SerializeField] private float duration;
    [SerializeField] private GameObject moneyEffect;
    public override void ExecuteActiveAbility()
    {
        FunctionTimer.Create(GoBackToMoneyMultiplier, duration);
        Destroy(Instantiate(moneyEffect, transform.position, Quaternion.identity), duration);
        GlobalBaseEffects.TempMoneyMultiplier = MoneyMultiplier;
        GetComponent<Base>().UpdateAbilityTimer();
        audioSrc.PlayOneShot(audioSrc.clip);
    }

    private void GoBackToMoneyMultiplier()
    {
        GlobalBaseEffects.TempMoneyMultiplier = 1;
    }
}
