using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyWorm : Enemy
{
    [SerializeField] private float abilityCd;
    [SerializeField] private float abilityDuration;

    private void Start()
    {
        base.Start();
        
        StartCoroutine(nameof(Borrow));
    }

    private IEnumerator Borrow()
    {
        // TODO: animator animation
        GetComponent<SpriteRenderer>().color = Color.blue;
        gameObject.layer = LayerMask.NameToLayer("Underground");
        isInvulnerable = true;
        EnemyMover.StartMovement();
        
        if (TryGetComponent<Fire>(out var fire))
            Destroy(fire);

        if (TryGetComponent<Freeze>(out var freeze))
        {
            freeze.UnfreezeInstantly();
            Destroy(freeze);
        }
        
        yield return new WaitForSeconds(abilityDuration);
        StartCoroutine(nameof(UnBorrow));
    }

    private IEnumerator UnBorrow()
    {
        // TODO: animator animation
        GetComponent<SpriteRenderer>().color = Color.red;
        gameObject.layer = LayerMask.NameToLayer("Enemy");
        isInvulnerable = false;
        EnemyMover.StopMovement();
        
        yield return new WaitForSeconds(abilityCd);
        StartCoroutine(nameof(Borrow));
    }
}