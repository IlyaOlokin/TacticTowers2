public class FreezeStats
{
    public int FreezeStacksNeeded { get; }
    public float FreezeTime { get; }
    public float FreezeStacksPerHit { get; }

    public FreezeStats(int freezeStacksNeeded, float freezeTime, float freezeStacksPerHit)
    {
        FreezeStacksNeeded = freezeStacksNeeded;
        FreezeTime = freezeTime;
        FreezeStacksPerHit = freezeStacksPerHit;
    }
}