using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TechButtonHighlight : MonoBehaviour
{
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
        if (Technologies.MinUpgradePrice < Credits.credits)
        {
            anim.SetTrigger("Highlight");
        }
    }

    
}
