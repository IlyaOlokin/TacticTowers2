using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Web : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float disarmDuration;
    [NonSerialized] public Vector3 endPos;
    private bool reachedEndPos = false;
    private CircleCollider2D collider;
    [SerializeField] private Sprite webSprite;
    
    private void Start()
    {
        collider = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, endPos, speed * Time.deltaTime);
        
        Vector3 dir = transform.position - endPos;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, angle + 90);
        
        if (transform.position.Equals(endPos) && !reachedEndPos)
        {
            OnTargetReached();
        }
    }

    private void OnTargetReached()
    {
        reachedEndPos = true;
        collider.enabled = true;
        GetComponent<SpriteRenderer>().sprite = webSprite;
        var towers = GameObject.FindGameObjectsWithTag("TowerInstance");
        foreach (var tower in towers)
        {
            var towerComp = tower.GetComponent<Tower>();

            if (Vector3.Distance(tower.transform.position, transform.position) <
                (transform.localScale.x + tower.transform.localScale.x) * 2
                && !towerComp.isDragging)
            {
                towerComp.Disarm(disarmDuration);
            }
        }

        StartCoroutine(Destroy(10));
    }

    private IEnumerator Destroy(float delay)
    {
        yield return new WaitForSeconds(delay);
        collider.radius = 0f;
        Destroy(gameObject);
    }
}
