using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Tesla : Tower
{
    [SerializeField] private GameObject lightning;
    public int lightningCount;
    public int bonusLightningCount;
    public float dmgDecrease;
    public float dmgDecreaseMultiplier;
    public float lightningJumpDistance;
    public float lightningJumpDistanceMultiplier;
    private DamageType damageType = DamageType.Fire;

    [NonSerialized] public bool hasSetOnFireUpgrade;
    [NonSerialized] public bool hasMicroStunUpgrade;
    
    [NonSerialized] public bool hasBranchingUpgrade;
    [Header("Branching Upgrade")]
    [SerializeField] private float branchingChance = 0.25f;

    private void Start() => audioSrc = GetComponent<AudioSource>();
    private new void Update() => base.Update();

    protected override void Shoot(GameObject enemy)
    {
        if (enemy == null) return;

        LootAtTarget(enemy.transform.position);

        if (shootDelayTimer <= 0)
        {
            object[] parms = {GetDmg(), transform.position, enemy, lightningCount + bonusLightningCount, new List<GameObject>()};
            StartCoroutine("ShootLightning", parms);
            shootDelayTimer = 1f / GetAttackSpeed();
            audioSrc.PlayOneShot(audioSrc.clip);
        }
    }

    private IEnumerator ShootLightning(object[] parms)
    {
        int lightningLeft = (int) parms[3];

        if (lightningLeft == 0) yield break;
        float dmg = (float) parms[0];
        Vector3 startPos = (Vector3) parms[1];
        GameObject enemy = (GameObject) parms[2];
        List<GameObject> pickedEnemies = (List<GameObject>) parms[4];


        var endPos = enemy.transform.position;
        

        var newLightning = Instantiate(lightning, transform.position, towerCanon.transform.rotation);
        newLightning.GetComponent<LineRenderer>().SetPosition(0, startPos);
        
        //AudioManager.Instance.Play("TeslaShot");
        
        if (CheckWallCollision(startPos, endPos, GetShootDistance(), false) is null)
        {
            newLightning.GetComponent<LineRenderer>().SetPosition(1, endPos);
            enemy.GetComponent<Enemy>().TakeDamage(dmg, damageType, transform.position);
            pickedEnemies.Add(enemy);
        }
        else
        {
            endPos = GetRayImpactPoint(startPos, endPos, false);
            newLightning.GetComponent<LineRenderer>().SetPosition(1, endPos);
            yield break;
        }
        Debug.DrawRay(startPos, endPos - startPos, Color.cyan, 1);
        

        

        yield return new WaitForSeconds(0.2f);
        int branches = 1;
        if (hasBranchingUpgrade && Random.Range(0f, 1f) < branchingChance)
        {
            branches = 2;
        }
        for (int i = 0; i < branches; i++)
        {
            GameObject newEnemy = null;
            var minDist = float.MaxValue;
            foreach (var e in EnemySpawner.enemies)
            {
                var distance = Vector3.Distance(endPos, e.transform.position);
                if (distance <= lightningJumpDistance * lightningJumpDistanceMultiplier && distance < minDist && !pickedEnemies.Contains(e))
                {
                    newEnemy = e;
                    minDist = distance;
                }
            }

            if (newEnemy == null) yield break;
            parms = new object[] {dmg * dmgDecrease * dmgDecreaseMultiplier, endPos, newEnemy, lightningLeft - 1, pickedEnemies};

            StartCoroutine("ShootLightning", parms);
        }
    }
}