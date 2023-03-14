using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HpCastle : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    const string scorePrefix = "";
    int score = 30;
    void Start()
    {
        scoreText.text = scorePrefix + score.ToString();
    }
    public int getHp()
    {
        return score;
    }
    public void addHp(int points)
    {
        score += points;
        scoreText.text = scorePrefix + score.ToString();
    }
    public void loseHp(int points)
    {
        score -= points;
        scoreText.text = scorePrefix + score.ToString();
    }
}
