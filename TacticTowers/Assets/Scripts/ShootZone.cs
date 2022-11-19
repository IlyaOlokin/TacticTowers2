using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class ShootZone : MonoBehaviour
{
    public Tower tower;
    private Image image;

    private bool needToTransform;

    private float currentFillAmount = 0;
    private float currentLocalScale = 0;
    private float currentEulerAnglesZ;
    
    private float targetFillAmount;
    private float targetLocalScale;
    private float targetEulerAnglesZ;

    private float fillAmountSpeed;
    private float localScaleSpeed;
    private float eulerAnglesSpeed;
    private float transformDuration = 0.7f;
    
    void Start()
    {
        image = GetComponent<Image>();
        image.enabled = true;
        currentEulerAnglesZ = tower.shootDirection;
        DrawShootZone();
    }

    private void Update()
    {
        if (!needToTransform) return;
        TransformShootZone();
    }

    public void DrawShootZone()
    {
        targetEulerAnglesZ = tower.shootDirection - tower.GetShootAngle() / 2f;
        targetFillAmount = tower.GetShootAngle() / 360f;
        targetLocalScale = tower.GetShootDistance() * 2.2f;

        fillAmountSpeed = Mathf.Abs(targetFillAmount - currentFillAmount) / transformDuration;
        localScaleSpeed = Mathf.Abs(targetLocalScale - currentLocalScale) / transformDuration;
        eulerAnglesSpeed = Mathf.Abs(targetEulerAnglesZ - currentEulerAnglesZ) / transformDuration;

        needToTransform = true;
    }

    private void TransformShootZone()
    {
        currentFillAmount = Mathf.MoveTowards(currentFillAmount, targetFillAmount, fillAmountSpeed * Time.deltaTime);
        image.fillAmount = currentFillAmount;

        currentLocalScale = Mathf.MoveTowards(currentLocalScale, targetLocalScale, localScaleSpeed * Time.deltaTime);
        transform.localScale = new Vector3(currentLocalScale, currentLocalScale);

        currentEulerAnglesZ =
            Mathf.MoveTowards(currentEulerAnglesZ, targetEulerAnglesZ, eulerAnglesSpeed * Time.deltaTime);
        transform.eulerAngles = new Vector3(0, 0, currentEulerAnglesZ);

        needToTransform = currentFillAmount != targetFillAmount || currentLocalScale != targetLocalScale ||
                          currentEulerAnglesZ != targetEulerAnglesZ;
    }
}
