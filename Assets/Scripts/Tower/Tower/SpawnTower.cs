using System.Collections;
using System.Collections.Generic;
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
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var cellPosDefault = spawnTilemap.WorldToCell(mousePos);
            var cellPosCenter = spawnTilemap.GetCellCenterWorld(cellPosDefault);
            if(spawnTilemap.GetColliderType(cellPosDefault) == Tile.ColliderType.Sprite)
            {
                spawnTilemap.SetColliderType(cellPosDefault, Tile.ColliderType.None);
                spawnTower(cellPosCenter,cellPosDefault);
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
    private void Update()
    {
        if (spawnId != -1)
        {
            DetectSpawnPoint();
        }
    }
}
