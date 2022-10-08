using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowingDownIncreasingHp : BasePassive
{
    [SerializeField] private SlowingDownIncreasingHpBox box;
    [SerializeField] private float SlowihgDownSpeed;
    [SerializeField] private float IncreasingHp;

    public override void ExecutePassiveEffect()
    {
        box.SlowihgDownSpeed = SlowihgDownSpeed;
        box.IncreasingHp = IncreasingHp;
    }
}
