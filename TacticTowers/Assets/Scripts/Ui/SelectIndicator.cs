using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectIndicator : MonoBehaviour
{
    private bool needToMove;
    private Vector3 destination;
    [SerializeField] private float speed;

    private void Update()
    {
        if (needToMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.unscaledDeltaTime);
        }

        if (transform.position == destination)
        {
            needToMove = false;
        }
    }

    public void GetNewDestination(Vector3 pos)
    {
        if (transform.position != pos)
        {
            needToMove = true;
            destination = pos;
        }
        
    }
}
