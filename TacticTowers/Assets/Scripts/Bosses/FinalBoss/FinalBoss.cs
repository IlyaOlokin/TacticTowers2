using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FinalBoss : Boss
{
    [SerializeField] private List<Enemy> bossParts;
    [SerializeField] private List<MonoBehaviour> bossFunctionality;
    [SerializeField] private List<Threshold> thresholds;
    [NonSerialized] public List<Transform> positions = new List<Transform>();
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        foreach (var pos in GameObject.FindGameObjectsWithTag("BossSpawnPoint"))
        {
            positions.Add(pos.transform);
        }
    }

    void Update()
    {
        UpdateHp();
    }

    protected override void UpdateHp()
    {
        hp = maxHp + GetDamageDone();
        for (int i = 0; i < thresholds.Count; i++)
        {
            if (!thresholds[i].passed && hp / maxHp < thresholds[i].hpThreshold)
            {
                anim.SetTrigger("ChangePosition");
                thresholds[i].passed = true;
            }
        }
        if (hp <= 0) Destroy(gameObject);
    }

    private float GetDamageDone()
    {
        float hp = 0;
        foreach (var bossPart in bossParts)
        {
            hp += bossPart.hp;
        }

        return hp;
    }

    public void ActivateBossFunctionality()
    {
        foreach (var component in bossFunctionality)
        {
            if (component == null) continue;
            component.enabled = true;
        }
    }
    
    public void DeactivateBossFunctionality()
    {
        foreach (var component in bossFunctionality)
        {
            if (component == null) continue;
            component.enabled = false;
        }
    }
}

[Serializable]
public class Threshold
{
    public float hpThreshold;
    [NonSerialized] public bool passed;
}
