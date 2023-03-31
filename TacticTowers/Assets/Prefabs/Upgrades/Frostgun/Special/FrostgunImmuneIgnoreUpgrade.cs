using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostgunImmuneIgnoreUpgrade : SpecialUpgrade
{
    public override void Execute(Tower tower) 
    {
        base.Execute(tower); 
        tower.gameObject.GetComponent<Frostgun>().hasImmuneIgnoreUpgrade = true;
    }
}
