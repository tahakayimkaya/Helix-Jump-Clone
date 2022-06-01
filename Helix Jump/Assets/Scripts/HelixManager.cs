using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelixManager : MonoBehaviour
{
    public GameObject[] helixRings;
    public float ySpawn = 27f;
    public float ringDistance = 2.5f;
    public int numberOfRings = 24;

    // Start is called before the first frame updates
    void Start()
    {
        //numberOfRings = GameController.currentLevelIndex + 5;
        for(int i=0; i < numberOfRings; i++)
        {
            if (i == 0)
                RingSpawner(0);
            else
                RingSpawner(Random.Range(1, helixRings.Length - 1));
        }
    }

    public void RingSpawner(int ringIndex)
    {
        GameObject go = Instantiate(helixRings[ringIndex], transform.up * ySpawn, Quaternion.identity);
        go.transform.parent = transform;
        ySpawn -= ringDistance;
    }
}
