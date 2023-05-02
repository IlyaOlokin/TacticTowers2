using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Enemy Set", menuName = "EnemySet")]
public class EnemySet : ScriptableObject
{
    public List<EnemyInfo> Right = new List<EnemyInfo>();
    public List<EnemyInfo> Top = new List<EnemyInfo>();
    public List<EnemyInfo> Left = new List<EnemyInfo>();
    public List<EnemyInfo> Bot = new List<EnemyInfo>();
}
