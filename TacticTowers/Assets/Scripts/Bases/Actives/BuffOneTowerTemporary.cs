using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffOneTowerTemporary : BaseActive
{
    [SerializeField] private GameObject box;
    [SerializeField] private float dmgMultiplier;
    [SerializeField] private float attackSpeedMultiplier;
    [SerializeField] private float shootAngleMultiplier;
    [SerializeField] private float shootDistanceMultiplier;

    [SerializeField] private float duration;

    public override void ExecuteActiveAbility()
    {
        UpdateOneTower();
    }

    private void UpdateOneTower()
    {
        box.SetActive(true);
        box.GetComponent<UpdateOneTower>().dmgMultiplier = dmgMultiplier;
        box.GetComponent<UpdateOneTower>().attackSpeedMultiplier = attackSpeedMultiplier;
        box.GetComponent<UpdateOneTower>().shootAngleMultiplier = shootAngleMultiplier; 
        box.GetComponent<UpdateOneTower>().shootDistanceMultiplier = shootDistanceMultiplier; 
        box.GetComponent<UpdateOneTower>().duration = duration;
    }
}
