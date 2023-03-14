
using UnityEngine;

public class UpdateTowerButton : MonoBehaviour
{
    public void UpdateTower()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();
        TowerEntity entity = gameManager.updateTower;
        entity.updateTower();
    }
}
