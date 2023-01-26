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

    [SerializeField] private GameObject buffEffect;
    [SerializeField] private GameObject chooseEffect;

    private void Start() => audioSrc = GetComponent<AudioSource>();
    
    public override void ExecuteActiveAbility()
    {
        UpdateOneTower();
        //audioSrc.PlayOneShot(audioSrc.clip);
    }

    private void UpdateOneTower()
    {
        var updateOneTower = box.GetComponent<UpdateOneTower>();
        updateOneTower.audioSrc = audioSrc;
        updateOneTower.dmgMultiplier = dmgMultiplier;
        updateOneTower.attackSpeedMultiplier = attackSpeedMultiplier;
        updateOneTower.shootAngleMultiplier = shootAngleMultiplier; 
        updateOneTower.shootDistanceMultiplier = shootDistanceMultiplier; 
        updateOneTower.duration = duration;
        updateOneTower.buffEffect = buffEffect;
        updateOneTower.chooseEffect = chooseEffect;
        box.SetActive(true);
    }
}
