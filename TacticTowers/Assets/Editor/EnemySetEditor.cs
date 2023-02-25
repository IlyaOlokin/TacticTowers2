using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnemySet))]
public class EnemySetEditor : Editor
{
    public override void OnInspectorGUI()
    {
        EnemySet enemySet = (EnemySet) target;
        
        EditorGUILayout.PropertyField(serializedObject.FindProperty("Right"), true);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("Top"), true);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("Left"), true);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("Bot"), true);
        
       

        float totalWeight = 0;
        totalWeight = CountTotalWeight(enemySet.Bot, totalWeight);
        totalWeight = CountTotalWeight(enemySet.Right, totalWeight);
        totalWeight = CountTotalWeight(enemySet.Top, totalWeight);
        totalWeight = CountTotalWeight(enemySet.Left, totalWeight);

        EditorGUILayout.FloatField("Total Weight:", totalWeight);
    }

    private static float CountTotalWeight(List<EnemyType> enemySet, float totalWeight)
    {
        foreach (var enemyType in enemySet)
        {
            if (enemyType.enemy == null) continue;
            totalWeight += enemyType.enemyCount * enemyType.enemy.GetComponent<Enemy>().GetWeight();
        }

        return totalWeight;
    }
}
