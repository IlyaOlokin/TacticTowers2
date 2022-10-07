using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LandscapeRandomiser : MonoBehaviour
{
    [SerializeField] private List<GameObject> places;
    [SerializeField] private List<GameObject> landscapes;
    
    // Start is called before the first frame update
    void Start()
    {
        foreach (var place in places)
        {
            var random = Random.Range(0, landscapes.Count);

            var land = Instantiate(landscapes[random], place.transform.position, place.transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
