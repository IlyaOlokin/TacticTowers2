using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    private LineRenderer lr;
    [NonSerialized] public GameObject target;
    [NonSerialized] public GameObject origin;
    [SerializeField] private GameObject impactEffect;
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.SetPosition(0, origin.transform.position);
        lr.SetPosition(1, Tower.GetRayImpactPoint(origin.transform.position,target.transform.position, false));
    }
    
    void Update()
    {
        if (target != null) lr.SetPosition(1, Tower.GetRayImpactPoint(origin.transform.position, target.transform.position, false));
        if (origin != null) lr.SetPosition(0, origin.transform.position);
        
        impactEffect.transform.position = lr.GetPosition(1);
    }

    public void IncreaseWidth(float heatCount)
    {
        lr.widthMultiplier = 1 + heatCount / 10;
    }
}
