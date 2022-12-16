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
        boxCreator.SetActive(true);
        audioSrc.PlayOneShot(audioSrc.clip);
    }
}
