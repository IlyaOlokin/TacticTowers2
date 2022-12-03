using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScaler : MonoBehaviour
{
    [SerializeField] private Vector2 startScale;
    [SerializeField] private Vector2 targetScale;
    [SerializeField] private float scaleTime;
    private float scaleSpeedX;
    private float scaleSpeedY;
    void Start()
    {
        scaleSpeedX = (targetScale.x - startScale.x) / scaleTime;
        scaleSpeedY = (targetScale.y - startScale.y) / scaleTime;
        transform.localScale = startScale;
    }
    
    void Update()
    {
        Scale();
    }

    private void Scale()
    {
        Vector2 newScale = new Vector2();
        newScale.x = Mathf.MoveTowards(transform.localScale.x, targetScale.x, scaleSpeedX * Time.deltaTime);
        newScale.y = Mathf.MoveTowards(transform.localScale.y, targetScale.y, scaleSpeedY * Time.deltaTime);
        transform.localScale = newScale;
    }
}
