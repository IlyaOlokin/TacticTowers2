using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyMoverAir : IEnemyMover
{
    private float speed;
    private float currentSpeed;
    private Vector3 target;
    
    public EnemyMoverAir(float initialSpeed, Vector3 initialTarget)
    {
        speed = initialSpeed;
        currentSpeed = initialSpeed;
        target = initialTarget;
    }

    public void Move(Transform transform)
    {
        const float fixedUpdateFreq = 50f;
        var currentPos = transform.position;
        transform.position = Vector3.MoveTowards(currentPos, target, currentSpeed / fixedUpdateFreq);
    }

    public void StartMovement()
    {
        ResetSpeed();
    }

    public void StopMovement()
    {
        currentSpeed = 0;
    }

    public void ChangeTarget(Vector3 newTarget)
    {
        target = newTarget;
    }
    
    public float GetRotationAngle(Vector3 currentPos)
    {
        var dest = (target - currentPos).normalized;
        return Mathf.Atan2(dest.y, dest.x) * Mathf.Rad2Deg;
    }

    public bool IsStopped()
    {
        return currentSpeed == 0;
    }

    public void RandomizeSpeed()
    {
        MultiplySpeed(Random.Range(1f, 1.75f));
    }

    public void MultiplySpeed(float multiplier)
    {
        speed *= multiplier;
        currentSpeed = speed;
    }

    public void ApplySlow(float slowAmount)
    {
        currentSpeed *= 1 - slowAmount;
    }
    
    public void ApplySlow(Func<float, float> slowFunc)
    {
        currentSpeed = slowFunc(speed);
    }

    public void ResetSpeed()
    {
        currentSpeed = speed;
    }
}