using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBim : MonoBehaviour
{
    private LineRenderer lr;
    [NonSerialized] public float Dmg;
    [NonSerialized] public GameObject target;
    [NonSerialized] public Vector3 origin;
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.SetPosition(0, origin);
    }
    
    void Update()
    {
        if (target != null) lr.SetPosition(1, target.transform.position);
        
    }
}
