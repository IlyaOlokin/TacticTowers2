using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tesla : Tower
{
    [SerializeField] private GameObject lightning;
    [SerializeField] public int lightningCount;
    [SerializeField] public float dmgDecrease;
    [SerializeField] public float lightningJumpDistance;

    void Update()
    {
        base.Update();
    }

    protected override void Shoot(GameObject enemy)
    {
        if (enemy == null) return;

        LootAtTarget(enemy);

        if (shootDelayTimer <= 0)
        {
            object[] parms = {GetDmg(), transform.position, enemy, lightningCount, new List<GameObject>()};
            StartCoroutine("ShootLightning", parms);
            shootDelayTimer = 1f / GetAttackSpeed();
        }
    }

    private IEnumerator ShootLightning(object[] parms)
    {
        int lightningLeft = (int) parms[3];

        if (lightningLeft == 0) yield break;
        float dmg = (float) parms[0];
        Vector3 startPos = (Vector3) parms[1];
        GameObject enemy = (GameObject) parms[2];
        List<GameObject> pickedEnemy = (List<GameObject>) parms[4];


        var endPos = enemy.transform.position;
        pickedEnemy.Add(enemy);

        var newLightning = Instantiate(lightning, transform.position, towerCanon.transform.rotation);
        newLightning.GetComponent<LineRenderer>().SetPosition(0, startPos);
        newLightning.GetComponent<LineRenderer>().SetPosition(1, endPos);

        enemy.GetComponent<Enemy>().TakeDamage(dmg);
        yield return new WaitForSeconds(0.2f);
        GameObject newEnemy = null;
        var minDist = float.MaxValue;
        foreach (var e in EnemySpawner.enemies)
        {
            var distance = Vector3.Distance(endPos, e.transform.position);
            if (distance <= lightningJumpDistance && distance < minDist && !pickedEnemy.Contains(e))
            {
                newEnemy = e;
                minDist = distance;
            }
        }

        if (newEnemy == null) yield break;
        parms = new object[] {dmg * dmgDecrease, endPos, newEnemy, lightningLeft - 1, pickedEnemy};
        StartCoroutine("ShootLightning", parms);
    }
}