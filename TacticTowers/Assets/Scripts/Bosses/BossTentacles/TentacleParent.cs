using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleParent : MonoBehaviour
{
    [SerializeField] private Transform boss;

    private void Start()
    {
        transform.parent = null;

    }

    private void Update()
    {
        transform.position = boss.position;
    }
}
