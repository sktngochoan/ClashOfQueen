using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurnCount : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    int score = 0;
    const string scorePrefix = "Turn: ";
    void Start()
    {
        scoreText.text = scorePrefix + score.ToString();
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        EnemySpawner enemySpawner = FindObjectOfType<EnemySpawner>();
        score = enemySpawner.turnNow();
        scoreText.text = scorePrefix + score.ToString();
    }
}
