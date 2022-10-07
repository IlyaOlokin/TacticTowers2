using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LandscapeRandomiser : MonoBehaviour
{
    [SerializeField] private GameObject landscape;
    [SerializeField] private List<GameObject> landscapes;

    void Start()
    {
        var landscapeTr = landscape.transform;
        var random = Random.Range(0, landscapes.Count);
        var land = Instantiate(landscapes[random], landscapeTr.position, landscapeTr.rotation, landscapeTr);
    }
}
