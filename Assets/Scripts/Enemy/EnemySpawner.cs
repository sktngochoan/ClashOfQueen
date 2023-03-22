using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private EnemySpawner instance;
    private int turn = 1;
    private int enemiesToSpawn = 5;
    private bool isSpawning = false;

    public float turnDelay = 60f;
    public float spawnInterval = 2f;
    public List<GameObject> prefabs;
    public GameObject boss;
    public int countTurn = 0;
    public Transform spawnPoints;

    void Awake()
    {
        instance = this;
    }

    public int turnNow()
    {
        return turn;
    }

    public int getTurnDelay()
    {
        return (int)turnDelay;
    }

    public void StartSpawning()
    {
        if (!isSpawning)
        {
            StartCoroutine(SpawnEnemies(enemiesToSpawn));
            isSpawning = true;
        }
    }

    IEnumerator SpawnDelay()
    {
        while (true)
        {
            yield return new WaitUntil(() => !isSpawning);
            yield return new WaitForSeconds(turnDelay);
            turn++;
            countTurn++;
            enemiesToSpawn += 3 ;
            StartSpawning();
        }
    }

    public void nextTurn()
    {
        StopAllCoroutines();
        isSpawning = false;
    }

    public bool CheckIsSpawning()
    {
        return isSpawning;
    }

    public void SpawnEnemiesNow()
    {
        StopAllCoroutines();
        isSpawning = false;
        turnDelay = 0;
        StartCoroutine(SpawnDelay());
        //turnDelay = 30f;
    }

    IEnumerator SpawnEnemies(int numEnemies)
    {
        int enemiesSpawned = 0;

        while (enemiesSpawned < numEnemies)
        {
            int randomPrefabID = Random.Range(0, prefabs.Count);
            GameObject spawnedEnemy = Instantiate(prefabs[randomPrefabID], spawnPoints);
            spawnedEnemy.transform.position = spawnPoints.transform.position;
            if (countTurn == 5)
            {
                GameObject spawnedEnemy2 = Instantiate(boss, spawnPoints);
                spawnedEnemy2.transform.position = spawnPoints.transform.position;
                countTurn = 0;
            }
            enemiesSpawned++;
            yield return new WaitForSeconds(spawnInterval);
        }

        isSpawning = false;
        turnDelay = 60f;

    }
    void Start()
    {
        StartCoroutine(SpawnDelay());
    }


}
