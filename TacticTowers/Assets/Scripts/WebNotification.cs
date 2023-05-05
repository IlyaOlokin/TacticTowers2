using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebNotification : MonoBehaviour
{
    [SerializeField] private Notification notification;
    [SerializeField] private Web web;

    private void OnEnable()
    {
        web.onTowerReached += TryShowNotification;
    }
    
    private void OnDisable()
    {
        web.onTowerReached -= TryShowNotification;
    }

    private void TryShowNotification()
    {
        if (Web.WebsReachedTower >= 2)
        {
            NotificationManager.Instance.GetNotification(notification);
        }
    }
}
