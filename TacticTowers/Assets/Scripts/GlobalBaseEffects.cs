using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalBaseEffects : MonoBehaviour
{
    private static float DmgMultiplierRight = 1;
    private static float DmgMultiplierUp = 1;
    private static float DmgMultiplierLeft = 1;
    private static float DmgMultiplierDown = 1;

    private static float AttackSpeedMultiplierRight = 1;
    private static float AttackSpeedMultiplierUp = 1;
    private static float AttackSpeedMultiplierLeft = 1;
    private static float AttackSpeedMultiplierDown = 1;

    private static float ShootAngleMultiplierRight = 1;
    private static float ShootAngleMultiplierUp = 1;
    private static float ShootAngleMultiplierLeft = 1;
    private static float ShootAngleMultiplierDown = 1;

    private static float ShootDistanceMultiplierRight = 1;
    private static float ShootDistanceMultiplierUp = 1;
    private static float ShootDistanceMultiplierLeft = 1;
    private static float ShootDistanceMultiplierDown = 1;

    public static float GetGlobalBaseDmgMultiplier(float angle)
    {
        switch (angle)
        {
            case 0:
                return DmgMultiplierRight;
            case 90:
                return DmgMultiplierUp;
            case 180:
                return DmgMultiplierLeft;
            case 270:
                return DmgMultiplierDown;
            default:
                Debug.Log("Wrong angle");
                return 0;
        }
    }
    
    public static float GetGlobalBaseAttackSpeedMultiplier(float angle)
    {
        switch (angle)
        {
            case 0:
                return AttackSpeedMultiplierRight;
            case 90:
                return AttackSpeedMultiplierUp;
            case 180:
                return AttackSpeedMultiplierLeft;
            case 270:
                return AttackSpeedMultiplierDown;
            default:
                Debug.Log("Wrong angle");
                return 0;
        }
    }
    
    public static float GetGlobalBaseShootAngleMultiplier(float angle)
    {
        switch (angle)
        {
            case 0:
                return ShootAngleMultiplierRight;
            case 90:
                return ShootAngleMultiplierUp;
            case 180:
                return ShootAngleMultiplierLeft;
            case 270:
                return ShootAngleMultiplierDown;
            default:
                Debug.Log("Wrong angle");
                return 0;
        }
    }
    
    public static float GetGlobalBaseShootDistanceMultiplier(float angle)
    {
        switch (angle)
        {
            case 0:
                return ShootDistanceMultiplierRight;
            case 90:
                return ShootDistanceMultiplierUp;
            case 180:
                return ShootDistanceMultiplierLeft;
            case 270:
                return ShootDistanceMultiplierDown;
            default:
                Debug.Log("Wrong angle");
                return 0;
        }
    }

    public static void ApplyToAllTowers(float dmgMultiplier, float attackSpeedMultiplier, float shootAngleMultiplier, float shootDistanceMultiplier)
    {
        DmgMultiplierRight = dmgMultiplier;
        DmgMultiplierUp = dmgMultiplier;
        DmgMultiplierLeft = dmgMultiplier;
        DmgMultiplierDown = dmgMultiplier;
        
        AttackSpeedMultiplierRight = attackSpeedMultiplier;
        AttackSpeedMultiplierUp = attackSpeedMultiplier;
        AttackSpeedMultiplierLeft = attackSpeedMultiplier;
        AttackSpeedMultiplierDown = attackSpeedMultiplier;
        
        ShootAngleMultiplierRight = shootAngleMultiplier;
        ShootAngleMultiplierUp = shootAngleMultiplier;
        ShootAngleMultiplierLeft = shootAngleMultiplier;
        ShootAngleMultiplierDown = shootAngleMultiplier;
        
        ShootDistanceMultiplierRight = shootDistanceMultiplier;
        ShootDistanceMultiplierUp = shootDistanceMultiplier;
        ShootDistanceMultiplierLeft = shootDistanceMultiplier;
        ShootDistanceMultiplierDown = shootDistanceMultiplier;
    }
    
    public static void ApplyToHorizontalTowers(float dmgMultiplier, float attackSpeedMultiplier, float shootAngleMultiplier, float shootDistanceMultiplier)
    {
        DmgMultiplierRight = dmgMultiplier;
        DmgMultiplierLeft = dmgMultiplier;

        AttackSpeedMultiplierRight = attackSpeedMultiplier;
        AttackSpeedMultiplierLeft = attackSpeedMultiplier;
       
        ShootAngleMultiplierRight = shootAngleMultiplier;
        ShootAngleMultiplierLeft = shootAngleMultiplier;

        ShootDistanceMultiplierRight = shootDistanceMultiplier;
        ShootDistanceMultiplierLeft = shootDistanceMultiplier;
    }
    
    public static void ApplyToVerticalTowers(float dmgMultiplier, float attackSpeedMultiplier, float shootAngleMultiplier, float shootDistanceMultiplier)
    {
        DmgMultiplierUp = dmgMultiplier;
        DmgMultiplierDown = dmgMultiplier;
        
        AttackSpeedMultiplierUp = attackSpeedMultiplier;
        AttackSpeedMultiplierDown = attackSpeedMultiplier;
        
        ShootAngleMultiplierUp = shootAngleMultiplier;
        ShootAngleMultiplierDown = shootAngleMultiplier;
  
        ShootDistanceMultiplierUp = shootDistanceMultiplier;
        ShootDistanceMultiplierDown = shootDistanceMultiplier;
    }
    
    
}
