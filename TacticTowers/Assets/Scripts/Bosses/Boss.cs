using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private float shootDelay = 2.5f;
    private bool canShoot = true;
    [SerializeField] private GameObject web;

    void Start()
    {

    }

    public void Shoot(GameObject obj)
    {
        if (obj == null) return;
        if(canShoot)
        {
            Instantiate(web, transform.position, transform.rotation);
            StartCoroutine(Reload());
        }   
    }

    IEnumerator Reload()
    {
        canShoot = false;
        yield return new WaitForSeconds(shootDelay);
        canShoot = true;
    }
}
