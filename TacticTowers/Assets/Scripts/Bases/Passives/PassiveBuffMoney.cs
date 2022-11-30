using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveBuffMoney : BasePassive
{
    [SerializeField] private float moneyAmount;
    
    void Update()
    {
        switch (Time.timeScale)
        {
            case 2:
                Money.AddMoney(moneyAmount * Time.deltaTime);
                break;
            case 4:
                Money.AddMoney(moneyAmount * Time.deltaTime);
                break;
        }
    }
}
