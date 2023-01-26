using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.AI;
using Debug = UnityEngine.Debug;

public class TowerDrag : MonoBehaviour
{
    private Collider2D coll2D;
    public Tower tower;
    private NavMeshObstacle navMeshObstacle;
    private Vector2 mouseOffset;

    private Vector3 pressStartPos;
    [NonSerialized] public bool dragging;
    private bool triedToDrag;
    [NonSerialized] public bool needToDrop;
        
    private bool hasConflicts;
    [SerializeField] private GameObject smokeEffect;
    private int edgeSize = 20;
    [SerializeField] private GameObject conflictIndicator;
    private AudioSource audioSrc;
    
    private void Start()
    {
        coll2D = GetComponent<CircleCollider2D>();
        navMeshObstacle = GetComponent<NavMeshObstacle>();
        audioSrc = GetComponent<AudioSource>();
    }
    
    private void Update()
    {
        if (needToDrop || Time.deltaTime == 0)
        {
            TryToDrop();
        }
        
        if (dragging)
        {
            var mousePos = Camera.main.ScreenToWorldPoint(ControlledMousePosition());
            transform.position = new Vector3(mousePos.x + mouseOffset.x, mousePos.y + mouseOffset.y);

            CheckForConflicts();
        }
        
        if (!triedToDrag) return;
        if (!dragging)
        {
            if (Vector2.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition), pressStartPos) >= 0.1f)
            {
                StartDragging();
            }
        }
    }

    private void CheckForConflicts()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, tower.transform.localScale.x * 2.5f);
        bool _hasConflicts = false;
        foreach (var hitEnemy in hitEnemies)
        {
            var otherGameObject = hitEnemy.gameObject;
            if (otherGameObject.CompareTag("Enemy") || otherGameObject.CompareTag("Base") ||
                otherGameObject.CompareTag("Tower") || otherGameObject.CompareTag("Wall"))
            {
                _hasConflicts = true;
                break;
            }
        }
        hasConflicts = _hasConflicts;
        conflictIndicator.SetActive(hasConflicts);
    }

    private Vector3 ControlledMousePosition()
    {
        var mousePos = Input.mousePosition;

        if (mousePos.x > Screen.width - edgeSize)
        {
            mousePos.x = Screen.width - edgeSize;
        }
        else if (mousePos.x < edgeSize)
        {
            mousePos.x = edgeSize;
        }

        if (mousePos.y > Screen.height - edgeSize)
        {
            mousePos.y = Screen.height - edgeSize;
        }
        else if (mousePos.y < edgeSize)
        {
            mousePos.y = edgeSize;
        }

        return mousePos;
    }

    private void OnMouseDown()
    {
        pressStartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        triedToDrag = true;
        IgnoreEnemiesToIgnore();
        mouseOffset =  transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void IgnoreEnemiesToIgnore()
    {
        foreach (var enemy in tower.enemiesToIgnore)
        {
            if (enemy is null) continue;
            Physics2D.IgnoreCollision(enemy.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }

    private void OnMouseUp()
    {
        TryToDrop();
    }

    private void TryToDrop()
    {
        if (!hasConflicts)
        {
            PlaceTower();
            needToDrop = false;
        }
        else if (!dragging)
        {
            needToDrop = false;
            triedToDrag = false;
        }
        else
        {
            needToDrop = true;
        }
    }

    private void PlaceTower()
    {
        if (dragging)
        {
            Instantiate(smokeEffect, transform.position, Quaternion.identity);
            audioSrc.PlayOneShot(audioSrc.clip);
        }
        dragging = false;
        tower.isDragging = false;
        triedToDrag = false;
        navMeshObstacle.enabled = true;
        coll2D.enabled = true;
    }

    private void StartDragging()
    {
        if (IsAnyOtherTowerDragging()) return;
        dragging = true;
        tower.isDragging = true;
        triedToDrag = false;
        navMeshObstacle.enabled = false;
        coll2D.enabled = false;
    }

    private bool IsAnyOtherTowerDragging()
    {
        var towers = FindObjectsOfType<TowerDrag>();
        foreach (var tower in towers)
        {
            if (tower.dragging) return true;
        }

        return false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var otherGameObject = other.gameObject;
        if (otherGameObject.CompareTag("Enemy") || otherGameObject.CompareTag("Base") || otherGameObject.CompareTag("Tower") || otherGameObject.CompareTag("Wall"))
        {
            IgnoreEnemiesToIgnore();
        }
    }
}
