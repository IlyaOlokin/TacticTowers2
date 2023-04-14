using UnityEngine;

public class EnemyFlying : Enemy
{
    public void Awake()
    {
        EnemyMover = new EnemyMoverAir(initialSpeed, GameObject.FindGameObjectWithTag("Base").transform.position);
        //ExecuteAbility();
    }

    public override void ExecuteAbility()
    {
        EnemyMover = new EnemyMoverAir(initialSpeed, GameObject.FindGameObjectWithTag("Base").transform.position);
    }
}