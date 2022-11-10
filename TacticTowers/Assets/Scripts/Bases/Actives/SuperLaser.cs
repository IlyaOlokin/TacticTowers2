using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperLaser : BaseActive
{
    [SerializeField] private GameObject box;
    [SerializeField] private float duration;

    public override void ExecuteActiveAbility()
    {
        CreateLaserBox();
    }

    private void CreateLaserBox()
    {
        box.SetActive(true);
        gameObject.GetComponent<Base>().UpdateAbilityTimer();
        FunctionTimer.Create(OffBox, duration);
    }

    private void OffBox()
    {
        box.SetActive(false);
    }
}
