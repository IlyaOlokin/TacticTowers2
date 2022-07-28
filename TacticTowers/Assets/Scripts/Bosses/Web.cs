using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Web : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    public Vector2 endPos;

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, endPos, speed * Time.deltaTime);
        if (transform.position.Equals(endPos))
            Destroy(gameObject, 1);
    }
}
