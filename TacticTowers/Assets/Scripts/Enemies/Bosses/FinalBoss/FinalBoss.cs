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
    [SerializeField] private float changePositionDelay;
    private float changePositionTimer;

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
        changePositionTimer += Time.deltaTime;
        if (changePositionTimer >= changePositionDelay) ChangePosition();
    }

    protected override void UpdateHp()
    {
        hp = maxHp + GetDamageDone();
        for (int i = 0; i < thresholds.Count; i++)
        {
            if (!thresholds[i].passed && hp / maxHp < thresholds[i].hpThreshold)
            {
                ChangePosition();
                thresholds[i].passed = true;
            }
        }
        if (hp <= 0) Destroy(gameObject);
    }

    private void ChangePosition()
    {
        anim.SetTrigger("ChangePosition");
        changePositionTimer = 0;
    }

    private float GetDamageDone()
    {
        return bossParts.Sum(bossPart => bossPart.GetHp());
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
