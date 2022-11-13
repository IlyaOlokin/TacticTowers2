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
        if (Input.GetMouseButton(1))
        {
            gameObject.SetActive(false);
        }

        if (Input.GetMouseButton(0))
        {
            isActive = true;
        }
        if (!isActive)
        {
            transform.position = Vector3.MoveTowards(transform.position, GetMousePosition(), 100f);
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
            Mine.GetComponent<Mine>().targetPos = new Vector3(GetMousePosition().x + rnd.Next(-100, 101) / 50f * transform.localScale.x,
                GetMousePosition().y + rnd.Next(-100, 101) / 50f * transform.localScale.y, 0);
            Instantiate(Mine, GameObject.FindGameObjectWithTag("Base").transform.position, Quaternion.identity);
        }
        GameObject.FindGameObjectWithTag("Base").GetComponent<Base>().UpdateAbilityTimer();
    }
    
    private Vector3 GetMousePosition()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        return mousePosition;
    }
}
