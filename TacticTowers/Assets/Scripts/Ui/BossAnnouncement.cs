using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnnouncement : MonoBehaviour
{
    private Animator anim;
    void OnEnable()
    {
        anim = GetComponent<Animator>();
        anim.Play("BossAnnouncementAppear");
        StartCoroutine(DeactivateThis(2.5f));
        AudioManager.Instance.Play("BossRoar1");
    }

    IEnumerator DeactivateThis(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }
}
