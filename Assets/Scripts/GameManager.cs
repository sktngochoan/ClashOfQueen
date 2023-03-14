using System.Collections;
using TMPro;
using UnityEngine;
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
    public Coint coint;
    GameObject update;
    void Awake() { instance = this; }
    public delegate void ButtonClickedEventHandler();


    public void OnButtonClicked()
    {
        if(coint.getCoint() < updateTower.getUpdatePrice())
        {
            Debug.Log("Need coint to update!");
        } else
        {
            updateTower.updateTower();
            TowerRange towerRange = updateTower.GetComponent<TowerRange>();
            towerRange.getCurrentRange();
            coint.loseCoint(updateTower.getUpdatePrice());
        }
        
    }
    public void OnButtonSellClick()
    {
        var position = spawnTilemap.WorldToCell(updateTower.gameObject.transform.position);
        spawnTilemap.SetColliderType(position, Tile.ColliderType.Sprite);
        coint.addCoint((updateTower.getPrice() + (updateTower.getLv() - 1) * updateTower.getUpdatePrice()) / 2);
        Destroy(updateTower.gameObject);
        updatePanel.SetActive(false);
    }
    void Start()
    {
        coint = GameObject.FindGameObjectWithTag("HUD1").GetComponent<Coint>();
        updatePanel = Instantiate(panelPrefab, canvas.transform);
        update = updatePanel.transform.Find("Update").gameObject;
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
        if(updateTower.getRange() == 0)
        {
            update.SetActive(false);
        }
        else
        {
            update.SetActive(true);
        }
        this.updateTower = updateTower;
    }
    public void setTextUpdatePanel(int sell, int update)
    {
        updateText.text = update + "$";
        sellText.text = sell + "$";
    }
}
