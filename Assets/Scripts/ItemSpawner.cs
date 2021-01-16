using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : Singleton<ItemSpawner>
{
    [SerializeField] private GameObject potionPrefab;
    [SerializeField] private int spawnNumber;
    [SerializeField] private bool useRandom;
    [SerializeField] private List<Transform> spawnPoints;

    private void Start()
    {
        SpawnItems(spawnNumber);
    }

    private void SpawnItems(int num)
    {
        for (int i = 0; i < num; i++)
        {
            GameObject potion = Instantiate(potionPrefab, transform);
            if (useRandom)
            {
                Vector2 position = RandomInSquare(new Vector2(.25f, 7.75f), new Vector2(.25f, 7.75f));
                potion.transform.position = new Vector3(position.x, .5f, position.y);
            }
            else
            {
                potion.transform.position = new Vector3(spawnPoints[i].position.x, .5f, spawnPoints[i].position.z);
            }
            
            potion.GetComponent<Potion>().StartAnimating();
        }
    }

    private Vector2 RandomInSquare(Vector2 xRange, Vector2 yRange)
    {
        return new Vector2(Random.Range(xRange.x, xRange.y), Random.Range(yRange.x, yRange.y));
    }
    
    
}
