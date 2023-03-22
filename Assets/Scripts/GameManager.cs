using System.Collections;
using System.Collections.Generic;
using System.IO;
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
    public Tilemap spawnTilemap;
    [SerializeField]
    public List<GameObject> enermyList;
    public Coint coint;
    GameObject update;
    private string filePath = "/Files/enermy.txt";
    private string filePathItems = "/Files/items.txt";
    void Awake() { instance = this; }
    public delegate void ButtonClickedEventHandler();

    public void OnButtonClicked()
    {
        if(coint.getCoint() < updateTower.getUpdatePrice())
        {
            AudioManager.Play(AudioClipName.BuyFail);
        } else
        {
            AudioManager.Play(AudioClipName.BuyTower);
            updateTower.updateTower();
            TowerRange towerRange = updateTower.GetComponent<TowerRange>();
            towerRange.getCurrentRange();
            coint.loseCoint(updateTower.getUpdatePrice());
        }
        
    }
    public void OnButtonSellClick()
    {
        AudioManager.Play(AudioClipName.BuyTower);
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
        checkLoadGame();
    }
    public void checkLoadGame()
    {
        if(PlayerPrefs.GetInt("isLoad") == 1)
        {
            SpawnTower spawnTower = FindObjectOfType<SpawnTower>();
            spawnTower.loadTower();
            spawnEnermy();
            loadItem();
        }
        else
        {
        }
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
    private void spawnEnermy()
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
                float hp = float.Parse(content1[i].Split(",")[3]);
                float speed = float.Parse(content1[i].Split(",")[4]);
                int type = int.Parse(content1[i].Split(",")[5]);
                int current = int.Parse(content1[i].Split(",")[6]);
                bool flip = bool.Parse(content1[i].Split(",")[7]);
                GameObject enermy = Instantiate(enermyList[type]);
                SpriteRenderer sprite = enermy.GetComponent<SpriteRenderer>();
                sprite.flipX = flip;
                enermy.transform.position = new Vector3(x, y, z);
                EnemeEntity entity = enermy.GetComponent<EnemeEntity>();
                EnermyWalk enermyWalk = enermy.GetComponent<EnermyWalk>();
                enermyWalk.setCurrentPoint(current);
                entity.hp = hp;
                entity.speed = speed;
            }
            catch
            {

            }

        }
    }
    private void loadItem()
    {
        string content = File.ReadAllText(Application.dataPath + filePathItems);
        string[] content1 = content.Split("\n");

        for (int i = 0; i < content1.Length; i++)
        {
            try
            {
                int currentCoint = int.Parse(content1[i].Split(",")[0]);
                int hp = int.Parse(content1[i].Split(",")[1]);
                coint.setCoint(currentCoint);
                HpCastle hpCastle = FindObjectOfType<HpCastle>();
                hpCastle.setHp(hp);
            }
            catch
            {
            }

        }
    }
}
