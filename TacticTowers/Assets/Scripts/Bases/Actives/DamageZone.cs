using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : BaseActive
{
    [SerializeField] private GameObject box;
    [SerializeField] private float duration;

    public override void ExecuteActiveAbility()
    {
        CreateDamageZone();
    }

    private void CreateDamageZone()
    {
        box.SetActive(true);
        FunctionTimer.Create(OffBox, duration);
    }

    private void OffBox()
    {
        box.SetActive(false);
        box.GetComponent<DamageZoneBox>().Off();
    }
}
