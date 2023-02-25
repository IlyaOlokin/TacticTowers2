
using UnityEngine;
using UnityEngine.AI;

public abstract class PathFinder
{
    protected NavMeshAgent agent;

    public PathFinder(NavMeshAgent agent)
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
    }
}