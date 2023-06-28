using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpScalerOnStart : MonoBehaviour
{
    public Vector3 targetScale;
    [SerializeField] private float lerpScaleSpeed = 1;
    [SerializeField] private bool useObjectScale;
    
    void Start()
    {
        if (useObjectScale) targetScale = transform.localScale;
        transform.localScale = new Vector3(0,0,0);
    }

    void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, lerpScaleSpeed * Time.deltaTime);
    }
}
