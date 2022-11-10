using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeEnemyTemporary : BaseActive
{
    [SerializeField] private FreezeEnemyTemporareBox box;
    [SerializeField] private float duration;
    [SerializeField] private float freezeStacksPerHit;
    [SerializeField] private int freezeStacksNeeded;

    public override void ExecuteActiveAbility()
    {
        box.freezeStacksNeeded = freezeStacksNeeded;
        box.freezeStacksPerHit = freezeStacksPerHit;
        box.freezeTime = duration;
        box.FreezeEnemy();
        gameObject.GetComponent<Base>().UpdateAbilityTimer();
    }
}
