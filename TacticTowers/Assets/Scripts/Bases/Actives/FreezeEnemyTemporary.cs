using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeEnemyTemporary : BaseActive
{
    [SerializeField] private FreezeEnemyTemporareBox box;
    [SerializeField] private float duration;
    [SerializeField] private float freezeStacksPerHit;
    [SerializeField] private int freezeStacksNeeded;
    [SerializeField] private GameObject freezeExplosion;

    public override void ExecuteActiveAbility()
    {
        box.freezeStacksNeeded = freezeStacksNeeded;
        box.freezeStacksPerHit = freezeStacksPerHit;
        box.freezeTime = duration;
        box.FreezeEnemy(); 
        GetComponent<Base>().UpdateAbilityTimer();
        Instantiate(freezeExplosion, transform.position, Quaternion.identity);
    }
}
