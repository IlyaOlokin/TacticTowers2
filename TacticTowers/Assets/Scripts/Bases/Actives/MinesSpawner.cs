using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class MinesSpawner : MonoBehaviour
{
    [NonSerialized] private bool isActive = false;
    [NonSerialized] public GameObject Mine;
    [NonSerialized] public int countMines;


    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            isActive = true;
        }
        if (!isActive)
        {
            transform.position = GetMousePosition();
        }
        else
        {
            SpawnMines();
            isActive = false;
            gameObject.SetActive(false);
        }
    }

    private void SpawnMines()
    {
        var rnd = new Random();
        for (var i = 0; i < countMines; i++)
        {
            var offset = new Vector3(transform.position.x + rnd.Next(-10, 11) / 10f, transform.position.y + rnd.Next(-10, 11) / 10f, 0);
            Instantiate(Mine, GameObject.FindGameObjectWithTag("Base").transform.position, Quaternion.identity);
            Mine.GetComponent<Mine>().targetPos = offset;
        }
    }
    
    private Vector3 GetMousePosition()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        return mousePosition;
    }
}
