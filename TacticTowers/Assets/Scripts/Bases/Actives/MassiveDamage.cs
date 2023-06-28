using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MassiveDamage : BaseActive
{
    [SerializeField] private GameObject box;
    [SerializeField] private float damage;
    [SerializeField] private GameObject explosion;

    private void Start() => audioSrc = GetComponent<AudioSource>();
        
    public override void ExecuteActiveAbility()
    {
        StartCoroutine("DealDamage", 0.2f);
        Instantiate(explosion, transform.position, Quaternion.identity);
        GetComponent<Base>().UpdateAbilityTimer();
        audioSrc.PlayOneShot(audioSrc.clip);
    }

    private IEnumerator DealDamage(float delay)
    {
        yield return new WaitForSeconds(delay);
        box.SetActive(true);
        box.GetComponent<MassiveDamageBox>().Explode(damage);
    }
}

