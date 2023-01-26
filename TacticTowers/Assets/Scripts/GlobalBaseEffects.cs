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
    
    private static float TempDmgMultiplierRight = 1;
    private static float TempDmgMultiplierUp = 1;
    private static float TempDmgMultiplierLeft = 1;
    private static float TempDmgMultiplierDown = 1;

    private static float TempAttackSpeedMultiplierRight = 1;
    private static float TempAttackSpeedMultiplierUp = 1;
    private static float TempAttackSpeedMultiplierLeft = 1;
    private static float TempAttackSpeedMultiplierDown = 1;

    private static float TempShootAngleMultiplierRight = 1;
    private static float TempShootAngleMultiplierUp = 1;
    private static float TempShootAngleMultiplierLeft = 1;
    private static float TempShootAngleMultiplierDown = 1;

    private static float TempShootDistanceMultiplierRight = 1;
    private static float TempShootDistanceMultiplierUp = 1;
    private static float TempShootDistanceMultiplierLeft = 1;
    private static float TempShootDistanceMultiplierDown = 1;

    public static float TempMoneyMultiplier = 1;

    public static void SetAllToDefault()
    {
         DmgMultiplierRight = 1;
         DmgMultiplierUp = 1;
         DmgMultiplierLeft = 1;
         DmgMultiplierDown = 1;

         AttackSpeedMultiplierRight = 1;
         AttackSpeedMultiplierUp = 1;
         AttackSpeedMultiplierLeft = 1;
         AttackSpeedMultiplierDown = 1;

         ShootAngleMultiplierRight = 1;
         ShootAngleMultiplierUp = 1;
         ShootAngleMultiplierLeft = 1;
         ShootAngleMultiplierDown = 1;

         ShootDistanceMultiplierRight = 1;
         ShootDistanceMultiplierUp = 1;
         ShootDistanceMultiplierLeft = 1;
         ShootDistanceMultiplierDown = 1;
        
         TempDmgMultiplierRight = 1;
         TempDmgMultiplierUp = 1;
         TempDmgMultiplierLeft = 1;
         TempDmgMultiplierDown = 1;

         TempAttackSpeedMultiplierRight = 1;
         TempAttackSpeedMultiplierUp = 1;
         TempAttackSpeedMultiplierLeft = 1;
         TempAttackSpeedMultiplierDown = 1;

         TempShootAngleMultiplierRight = 1;
         TempShootAngleMultiplierUp = 1;
         TempShootAngleMultiplierLeft = 1;
         TempShootAngleMultiplierDown = 1;

         TempShootDistanceMultiplierRight = 1;
         TempShootDistanceMultiplierUp = 1;
         TempShootDistanceMultiplierLeft = 1;
         TempShootDistanceMultiplierDown = 1;
    }
    
    public static float GetGlobalBaseDmgMultiplier(float angle)
    {
        switch (angle)
        {
            case 0:
                return DmgMultiplierRight * TempDmgMultiplierRight;
            case 90:
                return DmgMultiplierUp * TempDmgMultiplierUp;
            case 180:
                return DmgMultiplierLeft * TempDmgMultiplierLeft;
            case 270:
                return DmgMultiplierDown * TempDmgMultiplierDown;
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
                return AttackSpeedMultiplierRight * TempAttackSpeedMultiplierRight;
            case 90:
                return AttackSpeedMultiplierUp * TempAttackSpeedMultiplierUp;
            case 180:
                return AttackSpeedMultiplierLeft * TempAttackSpeedMultiplierLeft;
            case 270:
                return AttackSpeedMultiplierDown * TempAttackSpeedMultiplierDown;
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
                return ShootAngleMultiplierRight * TempShootAngleMultiplierRight;
            case 90:
                return ShootAngleMultiplierUp * TempShootAngleMultiplierUp;
            case 180:
                return ShootAngleMultiplierLeft * TempShootAngleMultiplierLeft;
            case 270:
                return ShootAngleMultiplierDown * TempShootAngleMultiplierDown;
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
                return ShootDistanceMultiplierRight * TempShootDistanceMultiplierRight;
            case 90:
                return ShootDistanceMultiplierUp * TempShootDistanceMultiplierUp;
            case 180:
                return ShootDistanceMultiplierLeft * TempShootDistanceMultiplierLeft;
            case 270:
                return ShootDistanceMultiplierDown * TempShootDistanceMultiplierDown;
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

    public static void ApplyToUpTowers(float dmgMultiplier, float attackSpeedMultiplier, float shootAngleMultiplier, float shootDistanceMultiplier)
    {
        DmgMultiplierUp = dmgMultiplier;

        AttackSpeedMultiplierUp = attackSpeedMultiplier;

        ShootAngleMultiplierUp = shootAngleMultiplier;

        ShootDistanceMultiplierUp = shootDistanceMultiplier;
    }

    public static void ApplyToDownTowers(float dmgMultiplier, float attackSpeedMultiplier, float shootAngleMultiplier, float shootDistanceMultiplier)
    {
        DmgMultiplierDown = dmgMultiplier;
        
        AttackSpeedMultiplierDown = attackSpeedMultiplier;
        
        ShootAngleMultiplierDown = shootAngleMultiplier;
        
        ShootDistanceMultiplierDown = shootDistanceMultiplier;
    }

    public static void ApplyToLeftTowers(float dmgMultiplier, float attackSpeedMultiplier, float shootAngleMultiplier, float shootDistanceMultiplier)
    {
        DmgMultiplierLeft = dmgMultiplier;

        AttackSpeedMultiplierLeft = attackSpeedMultiplier;

        ShootAngleMultiplierLeft = shootAngleMultiplier;

        ShootDistanceMultiplierLeft = shootDistanceMultiplier;
    }

    public static void ApplyToRightTowers(float dmgMultiplier, float attackSpeedMultiplier, float shootAngleMultiplier, float shootDistanceMultiplier)
    {
        DmgMultiplierRight = dmgMultiplier;

        AttackSpeedMultiplierRight = attackSpeedMultiplier;

        ShootAngleMultiplierRight = shootAngleMultiplier;

        ShootDistanceMultiplierRight = shootDistanceMultiplier;
    }

    public static void ApplyToAllTowersDmgMultiplier(float dmgMultiplier)
    {
        DmgMultiplierRight = dmgMultiplier;
        DmgMultiplierLeft = dmgMultiplier;
        DmgMultiplierUp = dmgMultiplier;
        DmgMultiplierDown = dmgMultiplier;
    }

    public static void ApplyToAllTowersAttackSpeedMultiplier(float attackSpeedMultiplier)
    {
        AttackSpeedMultiplierRight = attackSpeedMultiplier;
        AttackSpeedMultiplierLeft = attackSpeedMultiplier;
        AttackSpeedMultiplierUp = attackSpeedMultiplier;
        AttackSpeedMultiplierDown = attackSpeedMultiplier;
    }

    public static void ApplyToAllTowersShootAngleMultiplier(float shootAngleMultiplier)
    {
        ShootAngleMultiplierRight = shootAngleMultiplier;
        ShootAngleMultiplierLeft = shootAngleMultiplier;
        ShootAngleMultiplierUp = shootAngleMultiplier;
        ShootAngleMultiplierDown = shootAngleMultiplier;
    }

    public static void ApplyToAllTowersShootDistanceMultiplier(float shootDistanceMultiplier)
    {
        ShootDistanceMultiplierRight = shootDistanceMultiplier;
        ShootDistanceMultiplierLeft = shootDistanceMultiplier;
        ShootDistanceMultiplierUp = shootDistanceMultiplier;
        ShootDistanceMultiplierDown = shootDistanceMultiplier;
    }

    public static void ApplyToAllTowersTemporary(float dmgMultiplier, float attackSpeedMultiplier,
        float shootAngleMultiplier, float shootDistanceMultiplier, float buffDuration)
    {
        FunctionTimer.Create(GoBackToDefaultMultipliers, buffDuration);
        
        TempDmgMultiplierRight = dmgMultiplier;
        TempDmgMultiplierUp = dmgMultiplier;
        TempDmgMultiplierLeft = dmgMultiplier;
        TempDmgMultiplierDown = dmgMultiplier;
        
        TempAttackSpeedMultiplierRight = attackSpeedMultiplier;
        TempAttackSpeedMultiplierUp = attackSpeedMultiplier;
        TempAttackSpeedMultiplierLeft = attackSpeedMultiplier;
        TempAttackSpeedMultiplierDown = attackSpeedMultiplier;
        
        TempShootAngleMultiplierRight = shootAngleMultiplier;
        TempShootAngleMultiplierUp = shootAngleMultiplier;
        TempShootAngleMultiplierLeft = shootAngleMultiplier;
        TempShootAngleMultiplierDown = shootAngleMultiplier;
        
        TempShootDistanceMultiplierRight = shootDistanceMultiplier;
        TempShootDistanceMultiplierUp = shootDistanceMultiplier;
        TempShootDistanceMultiplierLeft = shootDistanceMultiplier;
        TempShootDistanceMultiplierDown = shootDistanceMultiplier;
    }

    public static void ApplyToHorizontalTowersTemporary(float dmgMultiplier, float attackSpeedMultiplier,
        float shootAngleMultiplier, float shootDistanceMultiplier, float buffDuration)
    { 
        FunctionTimer.Create(GoBackToDefaultMultipliers, buffDuration);

        TempDmgMultiplierRight = dmgMultiplier;
        TempDmgMultiplierLeft = dmgMultiplier;

        TempAttackSpeedMultiplierRight = attackSpeedMultiplier;
        TempAttackSpeedMultiplierLeft = attackSpeedMultiplier;
       
        TempShootAngleMultiplierRight = shootAngleMultiplier;
        TempShootAngleMultiplierLeft = shootAngleMultiplier;

        TempShootDistanceMultiplierRight = shootDistanceMultiplier;
        TempShootDistanceMultiplierLeft = shootDistanceMultiplier;
    }

    public static void ApplyToVerticalTowersTemporary(float dmgMultiplier, float attackSpeedMultiplier,
        float shootAngleMultiplier, float shootDistanceMultiplier, float buffDuration)
    {
        FunctionTimer.Create(GoBackToDefaultMultipliers, buffDuration);
        
        TempDmgMultiplierUp = dmgMultiplier;
        TempDmgMultiplierDown = dmgMultiplier;
        
        TempAttackSpeedMultiplierUp = attackSpeedMultiplier;
        TempAttackSpeedMultiplierDown = attackSpeedMultiplier;
        
        TempShootAngleMultiplierUp = shootAngleMultiplier;
        TempShootAngleMultiplierDown = shootAngleMultiplier;
  
        TempShootDistanceMultiplierUp = shootDistanceMultiplier;
        TempShootDistanceMultiplierDown = shootDistanceMultiplier;
    }

    public static void ApplyToUpTowersTemporary(float dmgMultiplier, float attackSpeedMultiplier, float shootAngleMultiplier, float shootDistanceMultiplier, float buffDuration)
    {
        FunctionTimer.Create(GoBackToDefaultMultipliers, buffDuration);

        TempDmgMultiplierUp = dmgMultiplier;

        TempAttackSpeedMultiplierUp = attackSpeedMultiplier;

        TempShootAngleMultiplierUp = shootAngleMultiplier;

        TempShootDistanceMultiplierUp = shootDistanceMultiplier;
    }

    public static void ApplyToDownTowersTemporary(float dmgMultiplier, float attackSpeedMultiplier, float shootAngleMultiplier, float shootDistanceMultiplier, float buffDuration)
    {
        FunctionTimer.Create(GoBackToDefaultMultipliers, buffDuration);

        TempDmgMultiplierDown = dmgMultiplier;

        TempAttackSpeedMultiplierDown = attackSpeedMultiplier;

        TempShootAngleMultiplierDown = shootAngleMultiplier;

        TempShootDistanceMultiplierDown = shootDistanceMultiplier;
    }

    public static void ApplyToLeftTowersTemporary(float dmgMultiplier, float attackSpeedMultiplier, float shootAngleMultiplier, float shootDistanceMultiplier, float buffDuration)
    {
        FunctionTimer.Create(GoBackToDefaultMultipliers, buffDuration);

        TempDmgMultiplierLeft = dmgMultiplier;

        TempAttackSpeedMultiplierLeft = attackSpeedMultiplier;

        TempShootAngleMultiplierLeft = shootAngleMultiplier;

        TempShootDistanceMultiplierLeft = shootDistanceMultiplier;
    }

    public static void ApplyToRightTowersTemporary(float dmgMultiplier, float attackSpeedMultiplier, float shootAngleMultiplier, float shootDistanceMultiplier, float buffDuration)
    {
        FunctionTimer.Create(GoBackToDefaultMultipliers, buffDuration);

        TempDmgMultiplierRight = dmgMultiplier;

        TempAttackSpeedMultiplierRight = attackSpeedMultiplier;

        TempShootAngleMultiplierRight = shootAngleMultiplier;

        TempShootDistanceMultiplierRight = shootDistanceMultiplier;
    }

    private static void GoBackToDefaultMultipliers()
    {
        TempDmgMultiplierRight = 1;
        TempDmgMultiplierUp = 1;
        TempDmgMultiplierLeft = 1;
        TempDmgMultiplierDown = 1;

        TempAttackSpeedMultiplierRight = 1;
        TempAttackSpeedMultiplierUp = 1;
        TempAttackSpeedMultiplierLeft = 1;
        TempAttackSpeedMultiplierDown = 1;

        TempShootAngleMultiplierRight = 1;
        TempShootAngleMultiplierUp = 1;
        TempShootAngleMultiplierLeft = 1;
        TempShootAngleMultiplierDown = 1;

        TempShootDistanceMultiplierRight = 1;
        TempShootDistanceMultiplierUp = 1;
        TempShootDistanceMultiplierLeft = 1;
        TempShootDistanceMultiplierDown = 1;
    }
}
