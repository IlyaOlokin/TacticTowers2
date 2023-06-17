using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tower Sprites", menuName = "TowerSprites")]
public class TowerSprites : ScriptableObject
{
    [field: SerializeField] public Sprite towerSprite { get; private set; }
    [field: SerializeField] public Sprite cannonSprite { get; private set; }
    [field: SerializeField] public Sprite basementSprite { get; private set; }
    [field: SerializeField] public Sprite additionalSprite { get; private set; }
}
