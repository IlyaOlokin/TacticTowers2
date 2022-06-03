using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TechButtonHighlight : MonoBehaviour
{
    private static Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
        TryHighlight();
    }

    public static void TryHighlight()
    {
        if (Technologies.MinUpgradePrice < Credits.credits)
        {
            anim.SetTrigger("Highlight");
        }
    }
}
