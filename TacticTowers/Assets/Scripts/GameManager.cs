using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform baseTransform;
    [SerializeField] private GameObject baseAbilityMenu;
    [SerializeField] private Button abilityButton;
    [SerializeField] private Image baseAbilityCoolDownImage;

    [SerializeField] private FinishPanel finishPanel;

    private void Awake()
    {
        var newBaseGameObject = Instantiate(BaseSelectManager.SelectedBase, baseTransform.position, Quaternion.identity, baseTransform);
        var newBase = newBaseGameObject.GetComponent<Base>();
        newBase.ExecuteBasePassiveEffect();
        finishPanel._base = newBase;
        newBase.baseAbilityMenu = baseAbilityMenu;
        newBase.abilityButton = abilityButton;
        newBase.coolDownImage = baseAbilityCoolDownImage;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            Money.AddMoney(100);
        }
    }
}
