using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceTower : MonoBehaviour
{
    private Vector3 shootingPosition;
    private Timer timer;
    private int lvl;
    private float speed;
    [SerializeField]
    private float range;
    private float damage;
    private float slow;
    private GameObject rangeCircle;
    private void Awake()
    {
        rangeCircle = transform.Find("range").gameObject;
        rangeCircle.SetActive(false);
        towerDefaul();
        changeRange(range);
        getTowerPosition();
    }
    private void getTowerPosition()
    {
        shootingPosition = transform.Find("ShootingPosition").position;

    }
    void Start()
    {
        getTowerPosition();
        Enemy enemy = GetClosestEnemy();
        if (enemy != null)
        {
            Bullet.Create(shootingPosition, enemy, 2);
        }
        timer = gameObject.AddComponent<Timer>();
        timer.Duration = speed;
        timer.Run();
    }
    void Update()
    {
        if (timer.Finished)
        {
            getTowerPosition();
            Enemy enemy = GetClosestEnemy();

            if (enemy != null)
            {
                Bullet.Create(shootingPosition, enemy, 2);
            }
            timer.Duration = speed;
            timer.Run();
        }
    }

    private Enemy GetClosestEnemy()
    {
        try
        {
            Enemy enemy = FindObjectOfType<Enemy>();
            if (enemy != null)
            {
                return Enemy.GetClosestEnemy(transform.position, range);
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
        Transform spriteTransform = transform.Find("range");
        spriteTransform.localScale = new Vector3(range * 18 / 11, range * 18 / 11, 0);
    }
    public void activeRange()
    {
        changeRange(range);
        rangeCircle.SetActive(true);
    }
    public void towerDefaul()
    {
        lvl = 1;
        speed = 0.5f;
        range = 5f;
        damage = 5f;
        slow = 2f;
    }

    public void updateTower()
    {
        lvl = lvl + 1;
        damage = 5f + (lvl * 0.2f);
        if ((lvl - 1) % 3 == 0)
        {
            range = 5f + 5f * lvl / 10;
        }
        if ((lvl - 1) % 5 == 0)
        {
            slow = 2f + (lvl/5 * 0.1f);
        }
        changeRange(range);
    }
    public float getSlow()
    {
        return slow;
    }
}
