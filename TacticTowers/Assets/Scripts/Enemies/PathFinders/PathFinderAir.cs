
using System;
using UnityEngine;
using UnityEngine.AI;

public class PathFinderAir : IPathFinder
{
    private float speed;
    private float currentSpeed;
    
    public PathFinderAir() { }

    public void StopMovement()
    {
        currentSpeed = 0;
    }

    public void StartMovement()
    {
        currentSpeed = speed;
    }

    public void SlowMovement(float slowAmount)
    {
        throw new NotImplementedException();
    }

    public float GetRotationAngle()
    {
        throw new NotImplementedException();
    }

    public bool IsStopped()
    {
        throw new NotImplementedException();
    }

    public void RandomizeSpeed()
    {
        throw new NotImplementedException();
    }

    public void MultiplySpeed(float multiplier)
    {
        throw new NotImplementedException();
    }

    public void ApplySlow(Func<float, float> slowFunc)
    {
        throw new NotImplementedException();
    }

    public void ResetSpeed()
    {
        throw new NotImplementedException();
    }
}