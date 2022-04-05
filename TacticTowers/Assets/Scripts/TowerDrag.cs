using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDrag : MonoBehaviour
{
    [SerializeField] private Tower tower;
    private Collider2D collider2D;
    private Vector2 mouseOffset;


    private void Start()
    {
        collider2D = GetComponent<CircleCollider2D>();
    }
    
    private void OnMouseDown()
    {
        tower.canShoot = false;
        collider2D.enabled = false;
        mouseOffset =  transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseDrag()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePos.x + mouseOffset.x, mousePos.y + mouseOffset.y);
    }

    private void OnMouseUp()
    {
        tower.canShoot = true;
        collider2D.enabled = true;
    }
}
