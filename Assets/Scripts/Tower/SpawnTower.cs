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
        DeSelectTower();
        spawnId = id;
        towerUI[spawnId].color = Color.white;
    }
    public void DeSelectTower()
    {
        spawnId = -1;
        for(int i = 0;i< towerUI.Count;i++)
        {
            towerUI[i].color = new Color(0.5f, 0.5f, 0.5f);
        }
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
                spawnTower(cellPosCenter);
            }
        }
    }
    private void spawnTower(Vector3 position)
    {
        GameObject tower = Instantiate(towerPrefabs[spawnId]);
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
