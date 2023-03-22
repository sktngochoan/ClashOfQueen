using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    public void PlayAgain()
    {
        SceneManager.LoadScene("GameScene");
        Time.timeScale = 1;
        GameObject[] list = GameObject.FindGameObjectsWithTag("Enermy");
        for(int i = 0; i < list.Length; i++)
        {
            EnemyList e = list[i].GetComponent<EnemyList>();
            EnemyList.RemoveEnemy(e);
        }
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene("MainScene");
        Time.timeScale = 1;
        GameObject[] list = GameObject.FindGameObjectsWithTag("Enermy");
        for (int i = 0; i < list.Length; i++)
        {
            EnemyList e = list[i].GetComponent<EnemyList>();
            EnemyList.RemoveEnemy(e);
        }
    }
}
