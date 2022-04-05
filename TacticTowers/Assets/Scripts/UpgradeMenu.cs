using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{
    private bool mouseOn;

    
    void Update()
    {
        if (Input.GetMouseButton(0) && !mouseOn)
        {
            gameObject.SetActive(false);
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
}
