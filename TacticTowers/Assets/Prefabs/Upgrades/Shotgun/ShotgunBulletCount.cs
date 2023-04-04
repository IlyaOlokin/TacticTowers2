using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunBulletCount : CommonUpgrade
{
    public override void Execute(Tower tower)
    {
        tower.GetComponent<Shotgun>().bonusBullets += (int) actualBonus;
    }
}
