using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nextTurnScript : MonoBehaviour
{
    public void nextTurn()
    {
        EnemySpawner enemySpawner = FindObjectOfType<EnemySpawner>();
        enemySpawner.SpawnEnemiesNow();
    }
}
