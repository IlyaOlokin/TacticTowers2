using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunBulletCount : Upgrade
{
    [SerializeField] private int bonus;

    public override void Execute(Tower tower)
    {
        tower.GetComponent<Shotgun>().bulletCount += bonus;
    }
}
