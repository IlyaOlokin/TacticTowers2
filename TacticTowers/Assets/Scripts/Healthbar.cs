using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private Enemy enemy;
    [SerializeField] private Slider slider;
    [SerializeField] private Color lowHpColor;
    [SerializeField] private Color highHpColor;
    [SerializeField] private Vector3 offset;
    
    public void SetHealth(float hp)
    {
        slider.gameObject.SetActive(hp < slider.maxValue);
        slider.value = hp;

        slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(lowHpColor, highHpColor, slider.normalizedValue);
    }

    private void Start()
    {
        canvas.worldCamera = Camera.main;
    }
    
    private void Update()
    {
        if (slider.maxValue < enemy.GetHp())
            slider.maxValue = enemy.GetHp();

        //slider.gameObject.SetActive(enemy.GetHp() < slider.maxValue);
        slider.transform.position = transform.parent.transform.position + offset;
    }
}