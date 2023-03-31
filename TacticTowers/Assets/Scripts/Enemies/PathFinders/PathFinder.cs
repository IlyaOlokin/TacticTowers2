
using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public abstract class PathFinder
{
    private float speed;
    protected NavMeshAgent agent;
    
    protected PathFinder(NavMeshAgent agent)
    {
        this.agent = agent;
        if (!agent.enabled || !agent.isOnNavMesh) 
            return;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.SetDestination(GameObject.FindGameObjectWithTag("Base").transform.position);
    }

    public void StopMovement()
    {
        agent.enabled = false;
    }

    public void StartMovement()
    {
        agent.enabled = true;
        agent.SetDestination(GameObject.FindGameObjectWithTag("Base").transform.position);
    }

    public void SlowMovement(float slowAmount)
    {
        agent.speed *= (1 - slowAmount);
    }
    
    public float GetRotationAngle()
    {
        return Mathf.Atan2(agent.desiredVelocity.y, agent.desiredVelocity.x) * Mathf.Rad2Deg;
    }

    public bool IsStopped() => !agent.enabled || agent.speed == 0;

    public void RandomizeSpeed()
    {
        var multiplier = Random.Range(1f, 1.75f);
        agent.speed *= multiplier;
        agent.avoidancePriority = (int) (agent.avoidancePriority * multiplier);
        speed = agent.speed;
    }

    public void MultiplySpeed(float multiplier)
    {
        agent.speed *= multiplier;
        speed = agent.speed;
    }

    public void ApplySlow(Func<float, float> slowFunc)
    {
        agent.speed = slowFunc(speed);
    }
    
    public void ResetSpeed()
    {
        agent.speed = speed;
    }
}