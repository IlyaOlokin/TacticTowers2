using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notification
{
    public Sprite sprite;
    public string text;

    public Notification(Sprite sprite, string text)
    {
        this.sprite = sprite;
        this.text = text;
    }
}
