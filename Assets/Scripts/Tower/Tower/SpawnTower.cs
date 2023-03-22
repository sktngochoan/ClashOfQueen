
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class SpawnTower : MonoBehaviour
{
    int spawnId = -1;
    [SerializeField]
    private List<GameObject> towerPrefabs;
    [SerializeField]
    private List<Image> towerUI;
    [SerializeField]
    private Tilemap spawnTilemap;
    private string filePath = "/Files/tower.txt";
    public void SelectTower(int id)
    {
        if(spawnId == id)
        {
            spawnId = -1;
            DeSelectTower();
        }
        else
        {
            DeSelectTower();
            spawnId = id;
            towerUI[spawnId].color = Color.white;
            TilemapRenderer tilemapRenderer = spawnTilemap.GetComponent<TilemapRenderer>();
            tilemapRenderer.material.SetColor("_Color", new Color(0.1296577f, 1f, 0f, 1f));
        }
        
    }
    public void DeSelectTower()
    {
        spawnId = -1;
        for(int i = 0;i< towerUI.Count;i++)
        {
            towerUI[i].color = new Color(0.5f, 0.5f, 0.5f);
        }
        TilemapRenderer tilemapRenderer = spawnTilemap.GetComponent<TilemapRenderer>();
        tilemapRenderer.material.SetColor("_Color", new Color(1f, 1f, 1f, 1f));
    }
    void DetectSpawnPoint()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameManager gameManager = FindObjectOfType<GameManager>();
            TowerEntity entity = towerPrefabs[spawnId].GetComponent<TowerEntity>();
            if(gameManager.coint.getCoint() < entity.getPrice())
            {
                AudioManager.Play(AudioClipName.BuyFail);
            }
            else
            {
                var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                var cellPosDefault = spawnTilemap.WorldToCell(mousePos);
                var cellPosCenter = spawnTilemap.GetCellCenterWorld(cellPosDefault);
                if (spawnTilemap.GetColliderType(cellPosDefault) == Tile.ColliderType.Sprite)
                {
                    spawnTilemap.SetColliderType(cellPosDefault, Tile.ColliderType.None);
                    spawnTower(cellPosCenter, cellPosDefault);
                    gameManager.coint.loseCoint(entity.getPrice());
                    AudioManager.Play(AudioClipName.BuyTower);
                }
            }
        }
    }
    private void spawnTower(Vector3 position,Vector3 cellPosDefault)
    {
        GameObject tower = Instantiate(towerPrefabs[spawnId]);
        TowerEntity entity = tower.GetComponent<TowerEntity>();
        entity.setPosition(cellPosDefault);
        tower.transform.position = position;
        DeSelectTower();

    }

    private void spawnTowerOnLoad(Vector3 position, Vector3 cellPosDefault,int lv,float speed)
    {
        GameObject tower = Instantiate(towerPrefabs[spawnId]);
        TowerEntity entity = tower.GetComponent<TowerEntity>();
        TowerRange range = tower.GetComponent<TowerRange>();
        entity.setLv(lv);
        entity.setOnLoad();
        range.changeRange(entity.getRange());
        entity.setPosition(cellPosDefault);
        tower.transform.position = position;
        DeSelectTower();

    }
    private void Update()
    {
        if (spawnId != -1)
        {
            DetectSpawnPoint();
        }
    }
    public void loadTower()
    {
        string content = File.ReadAllText(Application.dataPath + filePath);
        string[] content1 = content.Split("\n");

        for (int i = 0; i < content1.Length; i++)
        {
            try
            {
                float x = float.Parse(content1[i].Split(",")[0]);
                float y = float.Parse(content1[i].Split(",")[1]);
                float z = float.Parse(content1[i].Split(",")[2]);
                int lv = int.Parse(content1[i].Split(",")[3]);
                int type = int.Parse(content1[i].Split(",")[4]);
                int speed = int.Parse(content1[i].Split(",")[4]);
                spawnId = type;
                Vector3Int vector = new Vector3Int((int)x, (int)y, (int)z);
                var cellPosCenter = spawnTilemap.GetCellCenterWorld(vector);
                spawnTilemap.SetColliderType(vector, Tile.ColliderType.None);
                spawnTowerOnLoad(cellPosCenter, vector,lv, speed);
            }
            catch
            {

            }

        }
    }
}
