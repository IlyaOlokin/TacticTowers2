
using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyMoverGround : IEnemyMover
{
    private readonly NavMeshAgent agent;
    private Vector3 target;
    private float speed;

    public EnemyMoverGround(NavMeshAgent agent, float initialSpeed, Vector3 initialTarget)
    {
        this.agent = agent;
        speed = initialSpeed;
        agent.speed = speed;
        target = initialTarget;
        if (!agent.enabled || !agent.isOnNavMesh) 
            return;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }
    
    public void Move(Transform transform)
    {
        if (agent.enabled)
            agent.SetDestination(target);
    }
    
    public void StartMovement()
    {
        agent.enabled = true;
    }
    
    public void StopMovement()
    {
        agent.enabled = false;
    }

    public void ChangeTarget(Vector3 newTarget)
    {
        target = newTarget;
    }

    public float GetRotationAngle(Vector3 currentPos)
    {
        return Mathf.Atan2(agent.desiredVelocity.y, agent.desiredVelocity.x) * Mathf.Rad2Deg;
    }

    public bool IsStopped() => !agent.enabled || agent.speed == 0;

    public void RandomizeSpeed()
    {
        var multiplier = Random.Range(1f, 1.75f);
        agent.avoidancePriority = (int) (agent.avoidancePriority * multiplier);
        MultiplySpeed(multiplier);
    }

    public void MultiplySpeed(float multiplier)
    {
        speed *= multiplier;
        agent.speed = speed;
    }

    public void ApplySlow(Func<float, float> slowFunc)
    {
        agent.speed = slowFunc(speed);
    }
    
    public void ApplySlow(float slowAmount)
    {
        agent.speed *= 1 - slowAmount;
    }
    
    public void ResetSpeed()
    {
        agent.speed = speed;
    }
}
