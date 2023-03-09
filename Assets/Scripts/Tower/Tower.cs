using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.EventSystems;

public class Tower : MonoBehaviour
{

    private Vector3 shootingPosition;
    private Timer timer;
    private int lvl;
    private float speed;
    [SerializeField]
    private float range;
    private float damage;
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
        EnemyList enemy = GetClosestEnemy();
        if (enemy != null)
        {
            Bullet.Create(shootingPosition, enemy,1);
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
            EnemyList enemy = GetClosestEnemy();

            if (enemy != null)
            {
                Bullet.Create(shootingPosition, enemy,1);
            }
            timer.Duration = speed;
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
                return EnemyList.GetClosestEnemy(transform.position, range);
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
        spriteTransform.localScale = new Vector3(range * 18/11, range * 18 / 11, 0);
    }
    public void activeRange()
    {
        changeRange(range);
        rangeCircle.SetActive(true);
    }
    public void towerDefaul()
    {
        lvl = 1;
        speed = 1.5f;
        range = 7f;
        damage = 6f;
    }

    public void updateTower()
    {
        lvl = lvl + 1;
        damage = 6f + (lvl * 0.2f);
        if((lvl-1) % 3 == 0)
        {
            range = 7f + 7f * lvl / 10;
        }
        if((lvl - 1) % 5 == 0)
        {
            speed = 1.5f - (1.5f * 0.1f);
        }
        changeRange(range);
    }
}
