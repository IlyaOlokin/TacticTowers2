using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform baseTransform;
    [SerializeField] private GameObject baseAbilityMenu;
    [SerializeField] private Button abilityButton;
    [SerializeField] private Image baseAbilityCoolDownImage;
    [SerializeField] private Text baseAbilityCoolDownText;

    [SerializeField] private FinishPanel finishPanel;

    private void Awake()
    {
        GlobalBaseEffects.SetAllToDefault();
        var newBaseGameObject = Instantiate(BaseSelectManager.SelectedBase, baseTransform.position, Quaternion.identity, baseTransform);
        var newBase = newBaseGameObject.GetComponent<Base>();
        newBase.ExecuteBasePassiveEffect();
        finishPanel._base = newBase;
        newBase.baseAbilityMenu = baseAbilityMenu;
        newBase.abilityButton = abilityButton;
        abilityButton.image.sprite = newBase.GetComponent<BaseActive>().activeAbilitySprite;
        newBase.coolDownImage = baseAbilityCoolDownImage;
        newBase.coolDownText = baseAbilityCoolDownText;
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            Money.AddMoney(999999);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            var enemies = GameObject.FindWithTag("EnemyParent");
            for (var i = 0; i < enemies.transform.childCount; i++)
            {
                var enemy = enemies.transform.GetChild(i);
                enemy.GetComponent<Enemy>().TakeForce(100f, new Vector2(Random.Range(-1, 1), Random.Range(-1, 1)));
            }
        }
    }
}
