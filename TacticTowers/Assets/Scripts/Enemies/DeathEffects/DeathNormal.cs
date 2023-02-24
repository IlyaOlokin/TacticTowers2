using UnityEngine;

public class DeathNormal : IDeathEffect
{
    public void PlayEffect(Vector3 position, Vector3 killerPos)
    {
        var dir = position - killerPos;
        var asin = Mathf.Asin(dir.normalized.y);
        var degrees = asin * 180 / Mathf.PI;
        if (dir.x < 0) degrees = 180 - degrees;
        
        var rotation = Quaternion.Euler(0,0, degrees);
        Object.Instantiate(EnemyVFXManager.Instance.GetEffect("DeathNormal"), position, rotation);
    }
}
