using UnityEngine;
using System;

public class EnemyVFXManager : MonoBehaviour
{
    [SerializeField] private VisualEffect[] visualEffects;
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
        
        //DontDestroyOnLoad(gameObject);
    }

    public VisualEffect GetEffect(string effectName)
    {
        return Array.Find(visualEffects, effect => effect.name == effectName);
    }
}

[Serializable]
public class VisualEffect
{
    public string name;
    public GameObject effect;
}
