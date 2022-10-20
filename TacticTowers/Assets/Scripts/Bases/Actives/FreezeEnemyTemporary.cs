using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeEnemyTemporary : BaseActive
{
    [SerializeField] private FreezeEnemyTemporareBox box;
    [SerializeField] private float duration;

    public override void ExecuteActiveAbility()
    {
        box.FreezeEnemy(duration);

    }
}
