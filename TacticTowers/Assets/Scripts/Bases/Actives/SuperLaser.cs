using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperLaser : BaseActive
{
    [SerializeField] private GameObject box;
    [SerializeField] private GameObject boxCreator;

    private void Start() => base.Start();
    
    public override void ExecuteActiveAbility()
    {
        boxCreator.GetComponent<BoxCreator>().Box = box;
        boxCreator.SetActive(true);
        audioSrc.PlayOneShot(audioSrc.clip);
    }
}
