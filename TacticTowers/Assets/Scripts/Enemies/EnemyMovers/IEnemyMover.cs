using System;
using UnityEngine;

public interface IEnemyMover
{
    void Move(Transform transform);
    void StartMovement();
    void StopMovement();
    void ChangeTarget(Vector3 newTarget);
    float GetRotationAngle(Vector3 currentPos);
    bool IsStopped();
    void RandomizeSpeed();
    void MultiplySpeed(float multiplier);
    void ApplySlow(float slowAmount);
    void ApplySlow(Func<float, float> slowFunc);
    void ResetSpeed();
}