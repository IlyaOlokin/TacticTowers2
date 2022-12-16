using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseActive : MonoBehaviour
{
    
    public string description;
    public Sprite activeAbilitySprite;
    public float coolDown;

    protected AudioSource audioSrc;

    protected void Start() => audioSrc.GetComponent<AudioSource>();
    
    public virtual void ExecuteActiveAbility()
    {
        
    } 
}
