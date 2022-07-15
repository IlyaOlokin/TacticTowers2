using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    
    [SerializeField] private Transform BasePosition;
    [SerializeField] private FinishPanel finishPanel;

    private void Awake()
    {
        var newBase = Instantiate(BaseSelectManager.SelectedBase, BasePosition.position, Quaternion.identity);
        newBase.GetComponent<Base>().ExecuteBasePassiveEffect();
        finishPanel._base = newBase.GetComponent<Base>();
    }

    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            Money.AddMoney(100);
        }*/
    }
}
