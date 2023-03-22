
using UnityEngine;

public class TowerRange : MonoBehaviour
{
    private GameObject rangeCircle;
    private TowerEntity entity;
    private static GameObject activeRange;
    private void Awake()
    {
        rangeCircle = transform.Find("range").gameObject;
        entity = gameObject.GetComponent<TowerEntity>();
        changeRange(entity.getRange());
        rangeCircle.SetActive(false);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();
        getCurrentRange();
        rangeCircle.SetActive(true);
        if (activeRange == rangeCircle)
        {
            rangeCircle.SetActive(false);
            activeRange = null;
            gameManager.delectUpdatePanel();
        }
        else
        {
            if (activeRange != null)
            {
                activeRange.SetActive(false);
            }
            rangeCircle.SetActive(true);
            activeRange = rangeCircle;
            gameManager.selectUpdatePanel(gameObject.GetComponent<TowerEntity>());
            int price = entity.getPrice();
            int update = entity.getUpdatePrice();
            int lv = entity.getLv();
            gameManager.setTextUpdatePanel((price + (lv-1) * update) / 2, update) ;
        }
       
    }
    public void getCurrentRange()
    {
        Transform spriteTransform = rangeCircle.GetComponent<Transform>();
        spriteTransform.localScale = new Vector3(entity.getRange(), entity.getRange(), 0);
    }
    public void changeRange(float radius)
    {
        Transform spriteTransform = transform.Find("range");
        spriteTransform.localScale = new Vector3(radius, radius, 0);
    }
}
