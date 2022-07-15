using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseActive : MonoBehaviour
{
    protected Base _base;
    public string description;
    
    private void Start()
    {
        _base = GetComponent<Base>();
    }

    public virtual void ExecuteActiveAbility()
    {
        
    } 
}
