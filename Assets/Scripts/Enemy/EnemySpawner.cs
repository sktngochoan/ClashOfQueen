using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public EnemySpawner instance;
    void Awake() {instance= this;}
    //Enemy prefabs
    public List<GameObject> prefabs;
    //Enemy  spawn root pints
    public Transform spawnPoints;
    //Enemy spawn interval
    public float spawnInterval = 2f;

    public void StartSpawning()
    {
        //Call the spawn coroutine
        StartCoroutine(SpawnDelay()); 
        
        
    }
    IEnumerator SpawnDelay()
    {
        //call the spawn method
        SpawnEnemy();
        //wait spawn interval
        yield return new WaitForSeconds(spawnInterval);
        //recall the same coroutine
        StartCoroutine(SpawnDelay());

    }

    void SpawnEnemy()
    {
        //Randomsize the enemy spawned
        int randomPrefabID = Random.Range(0, prefabs.Count);
        //Randomsize the spawn poit
        //Instantiate the enemy prefab
        GameObject spawnedEnemy = Instantiate(prefabs[randomPrefabID],spawnPoints);
    }
}
