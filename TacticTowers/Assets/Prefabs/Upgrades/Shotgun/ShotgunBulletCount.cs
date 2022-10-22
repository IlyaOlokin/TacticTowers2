using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunBulletCount : Upgrade
{
    public override void Execute(Tower tower)
    {
        tower.GetComponent<Shotgun>().bulletCount += (int) actualBonus;
    }
}
