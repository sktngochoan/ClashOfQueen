using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class SettingsButton : MonoBehaviour
{
    public Tilemap spawnTilemap;
    string filePathTower = "/Files/tower.txt";
    string filePathEnemy = "/Files/enermy.txt";
    string filePathItems = "/Files/items.txt";
    public void Start()
    {
        Time.timeScale = 0;
        GameManager gameManager = FindObjectOfType<GameManager>();
        spawnTilemap = gameManager.spawnTilemap;
    }
    public void Resume()
    {
        Time.timeScale = 1;
        Destroy(gameObject);
    }
    public void Quiz()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void Settings()
    {

    }
    public void Save()
    {
        clearTowerFile();
        clearEnemyFile();
        clearItemsFile();
        loadTower();
        loadEnermy();
        loadItems();
    }
    private void loadEnermy()
    {
        GameObject[] Enermys = GameObject.FindGameObjectsWithTag("Enermy");
        if (Enermys.Length > 0)
        {

            using (StreamWriter writer = new StreamWriter(Application.dataPath + filePathEnemy, true))
            {
                for (int i = 0; i < Enermys.Length; i++)
                {
                    SpriteRenderer sprite = Enermys[i].GetComponent<SpriteRenderer>();
                    EnemeEntity entity = Enermys[i].GetComponent<EnemeEntity>();
                    EnermyWalk enermyWalk = Enermys[i].GetComponent<EnermyWalk>();
                    int current = enermyWalk.getCurrentPoint();
                    if(current == 12)
                    {
                        current--;
                    }
                    Vector3 position = Enermys[i].transform.position;
                    float hp = entity.getHp();
                    float speed = entity.getSpeed();
                    int type = entity.getType();
                    writer.WriteLine(position.ToString().TrimStart('(').TrimEnd(')').Trim(' ') + "," + hp + "," + speed + "," + type + "," + current + ","+ sprite.flipX);
                }
            }
        }
    }
    private void loadTower()
    {
        GameObject[] Towers = GameObject.FindGameObjectsWithTag("Tower");
        if (Towers.Length > 0)
        {

            using (StreamWriter writer = new StreamWriter(Application.dataPath + filePathTower, true))
            {
                for (int i = 0; i < Towers.Length; i++)
                {
                    TowerEntity entity = Towers[i].GetComponent<TowerEntity>();
                    int type = 0;
                    if (entity.getSlow() != 0)
                    {
                        type = 1;
                    }
                    else if (entity.getPoison() != 0)
                    {
                        type = 2;
                    }
                    else if (entity.getSpeed() == 0)
                    {
                        type = 3;
                    }
                    var position = spawnTilemap.WorldToCell(Towers[i].gameObject.transform.position);
                    writer.WriteLine(position.ToString().TrimStart('(').TrimEnd(')').Trim(' ') + "," + entity.getLv() + "," + type + "," + entity.getSpeed());
                }
            }
        }
    }
    private void loadItems()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();
        int coint = gameManager.coint.getCoint();
        HpCastle hpCastle = FindObjectOfType<HpCastle>();
        int hp = hpCastle.getHp();
        using (StreamWriter writer = new StreamWriter(Application.dataPath + filePathItems, true))
        {
            writer.WriteLine(coint + "," + hp);
        }

    }
    private void clearTowerFile()
    {
        File.WriteAllText(Application.dataPath + filePathTower, "");
    }
    private void clearEnemyFile()
    {
        File.WriteAllText(Application.dataPath + filePathEnemy, "");
    }
    private void clearItemsFile()
    {
        File.WriteAllText(Application.dataPath + filePathItems, "");
    }
}
