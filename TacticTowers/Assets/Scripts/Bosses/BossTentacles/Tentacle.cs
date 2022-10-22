using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Tentacle : MonoBehaviour
{
    [NonSerialized] public Enemy enemy;
    [SerializeField] private BossTentacles boss;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float moveSpeed;

    private Vector3 initialPosition;
    private Vector3 floatingDestination;
    private bool tentacleNeedToBeDisconnected;
    [SerializeField] private float floatingSpeed;
    [SerializeField] private float floatingSpread;

    private void Start()
    {
        initialPosition = transform.localPosition;
        floatingDestination = initialPosition;
    }

    void Update()
    {
        if (enemy != null)
        {
            tentacleNeedToBeDisconnected = true;
            MoveToEnemy();
        }
        else
        {
            if (tentacleNeedToBeDisconnected)
            {
                tentacleNeedToBeDisconnected = false;
                boss.DisconnectTentacle();
            }
            RandomMove();
        }
    }

    public void SetEnemyAsTarget(Enemy enemy)
    {
        this.enemy = enemy;
        boss.ConnectTentacle();
        enemy.SetTentacle();
    }

    private void RotateTowardsTarget(Vector3 vectorToTarget)
    {
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, rotationSpeed * Time.deltaTime);
    }

    private void MoveToEnemy()
    {
        transform.position =
            Vector3.MoveTowards(transform.position, enemy.transform.position, moveSpeed * Time.deltaTime);
        if (Vector2.Distance(transform.position, enemy.transform.position) < 0.001f)
        {
            if (boss == null) return;
            RotateTowardsTarget(enemy.transform.position - boss.transform.position);
        }
        else
            RotateTowardsTarget(enemy.transform.position - transform.position);
    }

    private void RandomMove()
    {
        if (transform.localPosition == floatingDestination)
        {
            var additionalVec = new Vector3(Random.Range(0, floatingSpread), Random.Range(0, floatingSpread));
            floatingDestination = initialPosition + additionalVec;
        }
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, floatingDestination, floatingSpeed * Time.deltaTime);
        RotateTowardsTarget(floatingDestination - transform.localPosition);
    }
}
