using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTransform : MonoBehaviour
{
    [SerializeField] private GameObject baseAbilityMenu;
    
    private void OnMouseDown()
    {
        baseAbilityMenu.SetActive(true);
    }
}
