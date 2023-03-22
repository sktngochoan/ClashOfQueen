using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class testLoad : MonoBehaviour
{
    public void loadSave()
    {
        PlayerPrefs.SetInt("isLoad", 1);
        SceneManager.LoadScene("GameScene");
    }
    public void startGame()
    {
        PlayerPrefs.SetInt("isLoad", 0);
        SceneManager.LoadScene("GameScene");
    }
    public void quitGame()
    {
        Application.Quit();
    }
    public void SetVolume(float volume)
    {
        Debug.Log(volume);
    }
}
