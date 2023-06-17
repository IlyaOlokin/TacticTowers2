using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class NotificationEnemies : MonoBehaviour
{
    [SerializeField] private float notificationDelay;
    private bool isNotificationAllowed = true;

    [SerializeField] private List<Notification> notifications;

    private void Start()
    {
        StartCoroutine(BlockNotification());
        StartCoroutine(SearchForNewEnemyTypes());
    }

    private IEnumerator SearchForNewEnemyTypes()
    {
        if (isNotificationAllowed && EnemySpawner.enemies.Count != 0)
            ScanEnemy(EnemySpawner.enemies[Random.Range(0, EnemySpawner.enemies.Count)].GetComponent<Enemy>());
        
        yield return null;
        StartCoroutine(SearchForNewEnemyTypes());
    }

    private void ScanEnemy(Enemy enemy)
    {
        switch (enemy.enemyType)
        {
            case EnemyType.Common:
                CreateNotification((int) EnemyType.Common);
                break;
            case EnemyType.Armor:
                CreateNotification((int) EnemyType.Armor);
                break;
            case EnemyType.Speed:
                CreateNotification((int) EnemyType.Speed);
                break;
            case EnemyType.Giant:
                CreateNotification((int) EnemyType.Giant);
                break;
            case EnemyType.Fly:
                CreateNotification((int) EnemyType.Fly);
                break;
            case EnemyType.Swarmer:
                CreateNotification((int) EnemyType.Swarmer);
                break;
            case EnemyType.Worm:
                CreateNotification((int) EnemyType.Worm);
                break;
            case EnemyType.Slime:
                CreateNotification((int) EnemyType.Slime);
                break;
            case EnemyType.ShieldBoss:
                CreateNotification((int) EnemyType.ShieldBoss);
                break;
            case EnemyType.WebBoss:
                CreateNotification((int) EnemyType.WebBoss);
                break;
            case EnemyType.ParasiteBoss:
                CreateNotification((int) EnemyType.ParasiteBoss);
                break;
            case EnemyType.FlyBoss:
                CreateNotification((int) EnemyType.FlyBoss);
                break;
            case EnemyType.FinalBoss:
                CreateNotification((int) EnemyType.FinalBoss);
                break;
            case EnemyType.Parasite:
                CreateNotification((int) EnemyType.Parasite);
                break;
            case EnemyType.None:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void CreateNotification(int index)
    {
        if (!Convert.ToBoolean(DataLoader.LoadInt("wasEnemyNotification" + index + "Shown", 0)))
        {
            NotificationManager.Instance.GetNotification(notifications[index]);
            DataLoader.SaveInt("wasEnemyNotification" + index + "Shown", 1);
            StartCoroutine(BlockNotification());
        }
    }

    private IEnumerator BlockNotification()
    {
        isNotificationAllowed = false;
        yield return new WaitForSeconds(notificationDelay);
        isNotificationAllowed = true;
    }
}