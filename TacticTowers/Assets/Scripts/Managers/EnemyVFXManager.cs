using UnityEngine;
using UnityEngine.Audio;
using System;
using System.Collections.Generic;
using System.Linq;

public class EnemyVFXManager : MonoBehaviour
{
    public VisualEffect[] VisualEffects;
    public static EnemyVFXManager Instance;
        
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    public GameObject GetEffect(string name)
    {
        return Array.Find(VisualEffects, effect => effect.Name == name).Effect;
    }
}

[Serializable]
public class VisualEffect
{
    public string Name;
    public GameObject Effect;

}
