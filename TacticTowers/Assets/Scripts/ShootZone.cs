using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootZone : MonoBehaviour
{
    [SerializeField] private Tower tower;
    private Image image;
    void Start()
    {
        image = GetComponent<Image>();
        image.enabled = true;
        DrawShootZone();
    }

    public void DrawShootZone()
    {
        image.fillAmount = tower.GetShootAngle() / 360f;
        transform.eulerAngles = new Vector3(0, 0, tower.shootDirection - tower.GetShootAngle() / 2f);
        transform.localScale = new Vector3(tower.GetShootDistance() * 2.2f, tower.GetShootDistance() * 2.2f, 1);
    }
}
