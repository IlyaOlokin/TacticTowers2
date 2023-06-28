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
        if (slider.maxValue < hp)
            slider.maxValue = hp;
        
        slider.value = hp;
        slider.gameObject.SetActive(hp < slider.maxValue);

        slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(lowHpColor, highHpColor, slider.normalizedValue);
    }

    public void ChangeBarVisibility(bool isVisible)
    {
        //if (slider.value < slider.maxValue)
            slider.gameObject.SetActive(isVisible);
    }
    
    private void Start()
    {
        canvas.worldCamera = Camera.main;
    }
    
    private void Update()
    {
        slider.transform.position = transform.parent.transform.position + offset;
    }
}