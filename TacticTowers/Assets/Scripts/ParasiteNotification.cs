using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParasiteNotification : MonoBehaviour
{
    [SerializeField] private Notification notification;
    
    void Update()
    {
        if (Parasite.ParasitesLifeTime >= 60)
        {
            NotificationManager.Instance.GetNotification(notification);
            Parasite.ParasitesLifeTime = 0;
        }
    }
}
