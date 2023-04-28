using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class NotificationPanel : MonoBehaviour, IPointerDownHandler
{
    private Animator anim;
    [SerializeField] private Image image;
    [SerializeField] private Text text;
    [SerializeField] private float lifeTime;
    private float lifeTimer;
    private bool isNotificationActive;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isNotificationActive)
        {
            lifeTimer += Time.unscaledDeltaTime;
            if (lifeTimer >= lifeTime)
            {
                HideNotification();
            }
        }
    }

    public void ShowNotification(Notification notification)
    {
        image.sprite = notification.sprite;
        text.text = notification.text;
        lifeTimer = 0;
        anim.SetTrigger("Show");
        isNotificationActive = true;
    }
    private void HideNotification()
    {
        anim.SetTrigger("Hide");
        isNotificationActive = false;
    }

    public void OnNotificationActivate()
    {
        NotificationManager.Instance.OnNotificationActivate();
    }
    
    public void OnNotificationDeactivate()
    {
        NotificationManager.Instance.OnNotificationDeactivate();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        HideNotification();
    }
}
