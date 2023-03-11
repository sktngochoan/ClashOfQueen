using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    
    public static GameManager instance;
    private TowerRange selectedTower;
    public GameObject panelPrefab;
    public Canvas canvas;
    public Text myText;
    private GameObject updatePanel;
    public TextMeshProUGUI updateText;
    private TextMeshProUGUI sellText;
    public TowerEntity updateTower;
    [SerializeField]
    private Tilemap spawnTilemap;
    void Awake() { instance = this; }
    public delegate void ButtonClickedEventHandler();

    public event ButtonClickedEventHandler ButtonClickedEvent;

    public void OnButtonClicked()
    {
        updateTower.updateTower();
        TowerRange towerRange = updateTower.GetComponent<TowerRange>();
        towerRange.getCurrentRange();
    }
    public void OnButtonSellClick()
    {
        var position = spawnTilemap.WorldToCell(updateTower.gameObject.transform.position);
        spawnTilemap.SetColliderType(position, Tile.ColliderType.Sprite);
        Destroy(updateTower.gameObject);
        updatePanel.SetActive(false);
    }
    void Start()
    {
        updatePanel = Instantiate(panelPrefab, canvas.transform);
        GameObject update = updatePanel.transform.Find("Update").gameObject;
        GameObject sell = updatePanel.transform.Find("Sell").gameObject;
        Button buttonUpdate = update.GetComponents<Button>()[0];
        Button buttonSell = sell.GetComponents<Button>()[0];
        buttonUpdate.onClick.AddListener(() => OnButtonClicked());
        buttonSell.onClick.AddListener(() => OnButtonSellClick());
        updateText = update.GetComponentInChildren<TextMeshProUGUI>();
        sellText = sell.GetComponentInChildren<TextMeshProUGUI>();
        updatePanel.SetActive(false);
        StartCoroutine(WaveStartDelay());
    }

    IEnumerator WaveStartDelay()
    {
        //Wait for X second
        yield return new WaitForSeconds(4f);
        //Start the enemy spawning
        GetComponent<EnemySpawner>().StartSpawning();   
    }

    public void changeUpdatePanel()
    {
        updatePanel.SetActive(!updatePanel.activeSelf);
    }
    public void delectUpdatePanel()
    {
        updatePanel.SetActive(false);
    }
    public void selectUpdatePanel(TowerEntity updateTower)
    {
        updatePanel.SetActive(true);
        this.updateTower = updateTower;
    }
    public void setTextUpdatePanel(int sell, int update)
    {
        updateText.text = update + "$";
        sellText.text = sell + "$";
    }
}
