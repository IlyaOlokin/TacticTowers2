using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyWorm : Enemy
{
    private SpriteRenderer spriteRenderer;

    [SerializeField] private float abilityCd;
    [SerializeField] private float abilityDuration;
    [SerializeField] [Range(0.0f, 1.0f)] private float abilityDurationVariance;
    [SerializeField] private Sprite defaultSprite;
    
    private void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();

        var abiltyDurVarNum = abilityDuration * abilityDurationVariance;
        abilityDuration = Random.Range(abilityDuration - abiltyDurVarNum, abilityDuration + abiltyDurVarNum);
        
        StartCoroutine(nameof(Borrow));
    }

    private IEnumerator Borrow()
    {
        animator.enabled = true;
        gameObject.layer = LayerMask.NameToLayer("Underground");
        isInvulnerable = true;
        GetComponentInChildren<Healthbar>().ChangeBarVisibility(false);
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
        animator.enabled = false;
        spriteRenderer.sprite = defaultSprite;
        gameObject.layer = LayerMask.NameToLayer("Enemy");
        isInvulnerable = false;
        GetComponentInChildren<Healthbar>().ChangeBarVisibility(true);
        EnemyMover.StopMovement();
        
        yield return new WaitForSeconds(abilityCd);
        StartCoroutine(nameof(Borrow));
    }
}