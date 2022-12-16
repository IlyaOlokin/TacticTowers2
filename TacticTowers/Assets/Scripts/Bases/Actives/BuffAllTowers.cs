using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffAllTowers : BaseActive
{
    
    [SerializeField] private float dmgMultiplier;
    [SerializeField] private float attackSpeedMultiplier;
    [SerializeField] private float shootAngleMultiplier;
    [SerializeField] private float shootDistanceMultiplier;

    [SerializeField] private float duration;

    [SerializeField] private GameObject buffEffect;
    private GameObject[] towers;

    private void Start()
    {
        towers = GameObject.FindGameObjectsWithTag("Tower");
        audioSrc = GetComponent<AudioSource>();
    }

    public override void ExecuteActiveAbility()
    {
        GlobalBaseEffects.ApplyToAllTowersTemporary(dmgMultiplier, attackSpeedMultiplier, shootAngleMultiplier,
            shootDistanceMultiplier, duration);
        GetComponent<Base>().UpdateAbilityTimer();
        CreateVisualEffect();
        audioSrc.PlayOneShot(audioSrc.clip);
    }

    private void CreateVisualEffect()
    {
        foreach (var tower in towers)
        {
            var newBuffEffect = Instantiate(buffEffect, tower.transform.position, Quaternion.identity, tower.transform);
            Destroy(newBuffEffect, duration);
        }
    }
}
