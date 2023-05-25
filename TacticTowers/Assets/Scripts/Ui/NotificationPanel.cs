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
    private RectTransform rt;
    private float lifeTimer;
    private bool isNotificationActive;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rt = image.transform.GetComponent<RectTransform>();
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
        Rect spriteRect = notification.sprite.rect;
        float aspectRatio = spriteRect.width / spriteRect.height;

        Vector2 newSizeDelta;
        if (aspectRatio > 1)
            newSizeDelta = new Vector2(75f, 75 / aspectRatio);
        else
            newSizeDelta = new Vector2(75 * aspectRatio, 75f);
        
        rt.sizeDelta = newSizeDelta;
        image.sprite = notification.sprite;
        text.text = Localisation.GetLocalisedValue(notification.text);
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
