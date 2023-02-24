using System.Collections;
using UnityEngine;

public class DeathFire : IDeathEffect
{
    public void PlayEffect(Vector3 position, Vector3 killerPos)
    {
        throw new System.NotImplementedException();
    }
    
    /*
    private void DieFire(Material newMaterial)
    {
        EnemySpawner.enemies.Remove(gameObject);
        GetComponent<SpriteRenderer>().material = newMaterial;
        agent.enabled = false;
        GetComponent<Collider2D>().enabled = false;
        foreach (Collider2D collider in transform.GetComponentsInChildren(typeof(Collider2D)))
        {
            collider.enabled = false;
        }
        StartCoroutine("Burn", GetComponent<SpriteRenderer>().material);
    }

    private IEnumerator Burn(Material material)
    {
        for (float alpha = fadeDuration; alpha >= 0; alpha -= Time.deltaTime)
        {
            material.SetFloat(fade, alpha / fadeDuration);
            yield return null;
        }
        Destroy(gameObject);
    }*/
}