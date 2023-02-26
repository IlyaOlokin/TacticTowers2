using System;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : BaseActive
{
    [SerializeField] private GameObject box;
    [SerializeField] private GameObject boxCreator;

    public override void ExecuteActiveAbility()
    {
        boxCreator.GetComponent<BoxCreator>().Box = box;
        boxCreator.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        boxCreator.GetComponent<BoxCreator>().baseActive = this;
        boxCreator.SetActive(true);
        isAiming = true;
    }

    public override void CancelAiming()
    {
        isAiming = false;
        boxCreator.SetActive(false);
    }
}
