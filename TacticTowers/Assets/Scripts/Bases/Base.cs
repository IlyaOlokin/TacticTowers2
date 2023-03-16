using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Base : MonoBehaviour
{
    [SerializeField] private Slider hpSlider;
    public float maxHp;
    [SerializeField] private Canvas canvas;
    [NonSerialized] public float hp;
    [NonSerialized] public GameObject baseAbilityMenu;
    [NonSerialized] public Button abilityButton;
    [NonSerialized] public Image coolDownImage;
    [NonSerialized] public Text coolDownText;
    public int unlockCost;
    
    private float abilityTimer = 0;
    private BaseActive ability;

    
    public Sprite baseImage;

    private void Awake()
    {
        maxHp *= Technologies.BaseHpMultiplier;
        hpSlider.maxValue = maxHp;
        hpSlider.value = maxHp;
        hp = maxHp;
    }

    private void Start()
    {
        canvas.worldCamera = Camera.main;
        abilityButton.onClick.AddListener(ExecuteBaseActiveAbility);
        ability = GetComponent<BaseActive>();
    }

    private void Update()
    {
        UpdateCoolUI();
        if (abilityTimer > 0)
            abilityTimer -= Time.deltaTime;
    }

    public void TakeDamage(float dmg)
    {
        hp -= dmg;
        UpdateHpBar();
        /*if (hp <= 0)
        {
            Destroy(gameObject);
        }*/
    }

    public void UpdateHpBar()
    {
        hpSlider.value = hp;
    }

    public void ExecuteBasePassiveEffect()
    {
        GetComponent<BasePassive>().ExecutePassiveEffect();
    }

    private void ExecuteBaseActiveAbility()
    {
        if (abilityTimer > 0) return;
        if (ability.isAiming)
        {
            ability.CancelAiming();
            return;
        }
            
        ability.ExecuteActiveAbility();
    }

    public void UpdateAbilityTimer()
    {
        abilityTimer = ability.coolDown;
    }

    private void UpdateCoolUI()
    {
        coolDownImage.fillAmount = 1 - (ability.coolDown - abilityTimer) / ability.coolDown;
        coolDownText.text = abilityTimer > 0 ? Mathf.Ceil(abilityTimer).ToString() : "";
    }
    
    private void OnMouseUp()
    {
        if (UiAppear.IsAnyUIActive()) return;
        OpenAbilityMenu();
    }

    private void OpenAbilityMenu()
    {
        baseAbilityMenu.SetActive(true);
    }

    public float GetHp() => hp;
    public float GetMaxHp() => maxHp;
}
