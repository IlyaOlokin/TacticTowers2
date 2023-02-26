using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCreator : MonoBehaviour
{
    [NonSerialized] private bool isActive = false;
    [NonSerialized] public GameObject Box;
    [NonSerialized] public BaseActive baseActive;

    private void Update()
    {
        if (Input.GetMouseButton(1))
        {
            baseActive.CancelAiming();
        }

        
        if (!isActive)
        {
            transform.position = Vector3.MoveTowards(transform.position, GetMousePosition(), 100f);
        }
        else
        {
            SpawnBox();
            isActive = false;
            gameObject.SetActive(false);
        }

        if (Input.GetMouseButtonUp(0))
        {
            isActive = true;
        }
    }

    private void SpawnBox()
    {
        baseActive.isAiming = false;
        Instantiate(Box, transform.position, Quaternion.identity);   
        GameObject.FindGameObjectWithTag("Base").GetComponent<Base>().UpdateAbilityTimer();
    }

    private Vector3 GetMousePosition()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        return mousePosition;
    }
}
