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
        box.GetComponent<DamageZoneBox>().duration = duration;
        
    }
}
