using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBim : MonoBehaviour
{
    private LineRenderer lr;
    [NonSerialized] public GameObject target;
    [NonSerialized] public Vector3 origin;
    [SerializeField] private GameObject impactEffect;
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.SetPosition(0, origin);
        lr.SetPosition(1, Tower.GetLaserImpactPoint(origin,target.transform.position));
    }
    
    void Update()
    {
        if (target != null) lr.SetPosition(1, Tower.GetLaserImpactPoint(origin, target.transform.position));
        impactEffect.transform.position = lr.GetPosition(1);
    }

    public void IncreaseWidth(float heatCount)
    {
        lr.widthMultiplier = 1 + heatCount / 10;
    }
}
