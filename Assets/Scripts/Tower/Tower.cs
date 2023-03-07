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
    private bool isHoldingDown = false;
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
            Enemy enemy = GetClosestEnemy();

            if (enemy != null)
            {
                Bullet.Create(shootingPosition, enemy,1);
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
        range = 11f;
        damage = 6f;
    }
}
