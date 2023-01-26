using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseAbilityMenu : MonoBehaviour
{
    private Animation anim;
    public bool mouseOn;
    
    private void OnEnable()
    {
        anim = GetComponent<Animation>();
        anim.Stop("UpgradeMenuAnimation");
        anim.Play("UpgradeMenuAnimation");
    }
    
    void Update()
    {
        if (Input.GetMouseButton(0) && !mouseOn)
        {
            DeactivateMenu();
        }
    }
    
    private void OnMouseEnter()
    {
        mouseOn = true;
    }

    private void OnMouseExit()
    {
        mouseOn = false;
    }

    private void DeactivateMenu()
    {
        gameObject.SetActive(false);
    }
}
