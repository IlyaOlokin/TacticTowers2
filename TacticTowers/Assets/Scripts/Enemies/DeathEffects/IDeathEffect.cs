using UnityEngine;

public interface IDeathEffect
{
    public void PlayEffect(GameObject source, Vector3 killerPos);
}