using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    public string upgradeLabel;
    [SerializeField] protected string upgradeText;
    
    public virtual void Execute(Tower tower)
    {
        
    }

    public string GetUpgradeText() => upgradeText;
    
}
