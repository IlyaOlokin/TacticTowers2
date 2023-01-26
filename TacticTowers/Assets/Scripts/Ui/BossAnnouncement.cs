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
        
        AudioManager.Instance.Play("BossRoar1");
    }

    public void DeactivateThis()
    {
        gameObject.SetActive(false);
    }
}
