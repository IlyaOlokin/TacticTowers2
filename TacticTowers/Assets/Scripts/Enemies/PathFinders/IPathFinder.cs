using System;

public interface IPathFinder
{
    void StopMovement();
    void StartMovement();
    void SlowMovement(float slowAmount);
    float GetRotationAngle();
    bool IsStopped();
    void RandomizeSpeed();
    void MultiplySpeed(float multiplier);
    void ApplySlow(Func<float, float> slowFunc);
    void ResetSpeed();
}