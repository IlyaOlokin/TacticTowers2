using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Tentacle : MonoBehaviour
{
    private Enemy enemy;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float moveSpeed;

    private Vector3 initialPosition;
    private Vector3 floatingDestination;
    [SerializeField] private float floatingSpeed;
    [SerializeField] private float floatingSpread;

    private void Start()
    {
        initialPosition = transform.localPosition;
        floatingDestination = initialPosition;
    }

    void Update()
    {
        if (enemy == null) RandomMove();
        else MoveToEnemy();
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
        RotateTowardsTarget(enemy.transform.position - transform.position);
    }

    private void RandomMove()
    {
        var additionalVec = new Vector3(Random.Range(0, floatingSpread), Random.Range(0, floatingSpread));
        if (transform.localPosition == floatingDestination) floatingDestination = initialPosition + additionalVec;
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, floatingDestination, floatingSpeed * Time.deltaTime);
        RotateTowardsTarget(floatingDestination - transform.localPosition);
    }
}
