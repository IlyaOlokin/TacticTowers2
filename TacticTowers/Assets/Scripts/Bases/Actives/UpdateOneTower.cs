using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateOneTower : MonoBehaviour
{
    private GameObject UpTower;
    [NonSerialized] public float dmgMultiplier;
    [NonSerialized] public float attackSpeedMultiplier;
    [NonSerialized] public float shootAngleMultiplier;
    [NonSerialized] public float shootDistanceMultiplier;

    [NonSerialized] public float duration;

    private bool isUp;
    private bool isActive;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Tower")) UpTower = other.gameObject;
    }

    private void Update()
    {
        if (!isActive) transform.position = GetMousePosition();

        if (UpTower && Input.GetMouseButton(0) && !isUp)
        {
            isActive = true;
            UpdateTower();
            isUp = true;
        }
    }

    private void UpdateTower()
    {
        switch (UpTower.GetComponent<TowerDrag>().tower.shootDirection)
        {
            case 0:
                GlobalBaseEffects.ApplyToRightTowersTemporary(dmgMultiplier, attackSpeedMultiplier,
                    shootAngleMultiplier, shootDistanceMultiplier, duration);
                break;
            case 90:
                GlobalBaseEffects.ApplyToUpTowersTemporary(dmgMultiplier, attackSpeedMultiplier,
                    shootAngleMultiplier, shootDistanceMultiplier, duration);
                break;
            case 180:
                GlobalBaseEffects.ApplyToLeftTowersTemporary(dmgMultiplier, attackSpeedMultiplier,
                    shootAngleMultiplier, shootDistanceMultiplier, duration);
                break;
            case 270:
                GlobalBaseEffects.ApplyToDownTowersTemporary(dmgMultiplier, attackSpeedMultiplier,
                    shootAngleMultiplier, shootDistanceMultiplier, duration);
                break;
        }

        UpTower.GetComponent<TowerDrag>().tower.shootZone.DrawShootZone();
        StartCoroutine(Return());
    }

    private IEnumerator Return()
    {
        yield return new WaitForSeconds(duration + 0.1f);
        isActive = false;
        isUp = false;
        UpTower.GetComponent<TowerDrag>().tower.shootZone.DrawShootZone();
        gameObject.SetActive(false);
    }

    private Vector3 GetMousePosition()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        return mousePosition;
    }
}
