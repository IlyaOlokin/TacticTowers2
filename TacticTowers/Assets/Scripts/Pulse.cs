using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulse : MonoBehaviour
{
    private Vector3 startScale;
    private Vector3 targetScale;
    [SerializeField] private float scaleMultiplier; 
    private float timer; 
    [SerializeField] private float scaleHalfLoopDuration; 
    private float scaleSpeed;
    [SerializeField] private bool useUnscaledTime;
    
    void Start()
    {
        startScale = transform.localScale;
        targetScale = startScale * scaleMultiplier;
        scaleSpeed = (startScale.x * scaleMultiplier - startScale.x) / scaleHalfLoopDuration;
    }

    
    void Update()
    {
        float deltaTime = useUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;
        timer += deltaTime;

        if (timer < scaleHalfLoopDuration)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, targetScale, scaleSpeed * deltaTime);
        }
        else if (timer > scaleHalfLoopDuration)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, startScale, scaleSpeed * deltaTime);

            if (timer > scaleHalfLoopDuration * 2f)
                timer = 0;
        }
    }
}
