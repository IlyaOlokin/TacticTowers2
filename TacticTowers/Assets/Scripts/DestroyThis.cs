using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyThis : MonoBehaviour
{
    [SerializeField] private float delay;
    [SerializeField] private bool releaseChildren;
    void Start()
    {
        Destroy(gameObject, delay);
    }
    
    void OnDestroy()
    {
        if (releaseChildren) transform.DetachChildren();
    }
}
