using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField]
    private GameObject updatePanel;
    [SerializeField]
    private Bullet bullet;
    private Vector3 shootingPosition;
    private Timer timer;
    private TowerEntity entity;
    private void Awake()
    {
        entity = gameObject.GetComponent<TowerEntity>();
        changeRange(entity.getRange());
        getTowerPosition();
    }
    private void getTowerPosition()
    {
        shootingPosition = transform.Find("ShootingPosition").position;

    }
    void Start()
    {
        getTowerPosition();
        EnemyList enemy = GetClosestEnemy();
        if (enemy != null)
        {
            bullet.Create(shootingPosition, enemy, 1, 0, 0,entity.getDamage());
            AudioManager.Play(AudioClipName.TowerShoot);
        }
        timer = gameObject.AddComponent<Timer>();
        timer.Duration = entity.getSpeed();
        timer.Run();
    }
    void Update()
    {
        if (timer.Finished)
        {
            getTowerPosition();
            EnemyList enemy = GetClosestEnemy();

            if (enemy != null)
            {
                AudioManager.Play(AudioClipName.TowerShoot);
                bullet.Create(shootingPosition, enemy, 1, 0, 0, entity.getDamage());
            }
            timer.Duration = entity.getSpeed();
            timer.Run();
        }
    }

    private EnemyList GetClosestEnemy()
    {
        try
        {
            EnemyList enemy = FindObjectOfType<EnemyList>();
            if (enemy != null)
            {
                return EnemyList.GetClosestEnemy(transform.position, entity.getRange());
            }
            else
            {
                return null;
            }
        }
        catch (System.Exception)
        {
        }
        return null;

    }
    public void changeRange(float radius)
    {
        TowerRange towerRange = gameObject.GetComponentInChildren<TowerRange>();
        towerRange.changeRange(radius);
    }
}
