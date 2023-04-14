using UnityEngine;

public class EnemyFlying : Enemy
{
    public void Awake()
    {
        ExecuteAbility();
    }

    public override void ExecuteAbility()
    {
        EnemyMover = new EnemyMoverAir(initialSpeed, GameObject.FindGameObjectWithTag("Base").transform.position);
    }
}