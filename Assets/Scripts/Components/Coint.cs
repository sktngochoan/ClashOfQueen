using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Coint : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    const string scorePrefix = "";
    int score = 200;
    void Start()
    {
        scoreText.text = scorePrefix + score.ToString();
    }
    public int getCoint()
    {
        return score;
    }
    public void setCoint(int score)
    {
        this.score = score;
    }
    public void addCoint(int points)
    {
        score += points;
        scoreText.text = scorePrefix + score.ToString();
    }
    public void loseCoint(int points)
    {
        score -= points;
        scoreText.text = scorePrefix + score.ToString();
    }
}
