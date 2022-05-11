using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDrag : MonoBehaviour
{
    public Tower tower;
    private Collider2D collider2D;
    private Vector2 mouseOffset;

    private Vector3 pressStartPos;
    private bool dragging;
    private bool triedToDrag;

    private int conflicts;

    private void Start()
    {
        collider2D = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        if (dragging)
        {
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePos.x + mouseOffset.x, mousePos.y + mouseOffset.y);
        }
        
        if (!triedToDrag) return;
        if (!dragging)
        {
            if (Vector2.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition), pressStartPos) >= 0.2f)
            {
                StartDragging();
            }
        }
    }

    private void OnMouseDown()
    {
        pressStartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        triedToDrag = true;
        collider2D.isTrigger = true;
        mouseOffset =  transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseUp()
    {
        if (conflicts == 0)
        {
            PlaceTower();
        }
    }

    private void PlaceTower()
    {
        tower.canShoot = true;
        collider2D.isTrigger = false;
        dragging = false;
        triedToDrag = false;
    }

    private void StartDragging()
    {
        dragging = true;
        tower.canShoot = false;
        triedToDrag = false;
        Debug.Log("!");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var otherGameObject = other.gameObject;
        if (otherGameObject.CompareTag("Enemy") || otherGameObject.CompareTag("Base"))
        {
            conflicts += 1;
            otherGameObject.transform.FindChild("ConflictIndicator").gameObject.SetActive(false);

            
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        var otherGameObject = other.gameObject;
        if (otherGameObject.CompareTag("Enemy") || otherGameObject.CompareTag("Base"))
        {
            conflicts -= 1;
            otherGameObject.transform.FindChild("ConflictIndicator").gameObject.SetActive(false);

        }
    }
}
