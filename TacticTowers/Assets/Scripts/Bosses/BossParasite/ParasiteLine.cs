using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParasiteLine : MonoBehaviour
{
    private Material material;
    private float disappearSpeed = 8;
    private float disappearAmount = 1;
    void Start()
    {
        material = GetComponent<LineRenderer>().material;
    }

    
    void Update()
    {
        disappearAmount += disappearSpeed * Time.deltaTime;
        material.SetFloat("_Disappear",  disappearAmount);
    }
}
