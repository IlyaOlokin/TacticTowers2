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

    }
}
