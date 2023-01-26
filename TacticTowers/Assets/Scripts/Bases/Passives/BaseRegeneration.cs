using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseRegeneration : BasePassive
{
    [SerializeField] private float regenAmount; // hp per sec
    private Base _base;
    
    private void Start()
    {
        _base = GetComponent<Base>();
    }
    
    void Update()
    {
        RegenerateBase();
    }

    private void RegenerateBase()
    {
        if (_base.hp >= _base.maxHp)
        {
            _base.hp = _base.maxHp;
            return;
        }

        _base.hp += regenAmount * Time.deltaTime;
        _base.UpdateHpBar();
    }

    public override void ExecutePassiveEffect()
    {
        
    }
}
