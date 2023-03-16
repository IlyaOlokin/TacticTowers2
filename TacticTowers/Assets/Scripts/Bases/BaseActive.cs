using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseActive : MonoBehaviour
{
    
    public string description;
    public Sprite activeAbilitySprite;
    public float coolDown;
    [NonSerialized] public bool isAiming;

    protected AudioSource audioSrc;

    public virtual void ExecuteActiveAbility()
    {
        
    } 

    public virtual void CancelAiming()
    {

    }
}
