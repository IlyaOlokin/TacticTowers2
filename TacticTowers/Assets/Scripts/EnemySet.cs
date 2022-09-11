using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Enemy Set", menuName = "EnemySet")]
public class EnemySet : ScriptableObject
{
    public List<EnemyType> Right = new List<EnemyType>();
    public List<EnemyType> Top = new List<EnemyType>();
    public List<EnemyType> Left = new List<EnemyType>();
    public List<EnemyType> Bot = new List<EnemyType>();
}
