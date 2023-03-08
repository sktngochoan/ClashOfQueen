using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public static GameManager instance;

    void Awake() { instance = this; }

    void Start()
    {
        StartCoroutine(WaveStartDelay());
    }

    IEnumerator WaveStartDelay()
    {
        //Wait for X second
        yield return new WaitForSeconds(4f);
        //Start the enemy spawning
        GetComponent<EnemySpawner>().StartSpawning();   
    }
}
