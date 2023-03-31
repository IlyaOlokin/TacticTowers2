using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailgunKnockBackUpgrade : SpecialUpgrade
{
    public override void Execute(Tower tower)
    {
        base.Execute(tower);
        tower.transform.GetComponent<Railgun>().hasKonockBackUpgrade = true;
    }
}
