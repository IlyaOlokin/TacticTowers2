using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveBuffMoney : BasePassive
{
    [SerializeField] private float moneyAmount;
    
    void Update()
    {
        if (Time.timeScale > 1) Money.AddMoney(moneyAmount * Time.deltaTime);
    }
}
