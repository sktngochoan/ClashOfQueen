using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonTower : MonoBehaviour
{
    private Vector3 shootingPosition;
    private Timer timer;

    private void Awake()
    {
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
            Bullet.Create(shootingPosition, enemy, 3);
        }
        timer = gameObject.AddComponent<Timer>();
        timer.Duration = 2f;
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
                Bullet.Create(shootingPosition, enemy, 3);
            }
            timer.Run();
        }

    }

    private Enemy GetClosestEnemy()
    {
        Enemy enemy = FindObjectOfType<Enemy>();

        if (enemy != null)
        {
            return Enemy.GetClosestEnemy(transform.position, 10f);
        }
        else
        {
            return null;
        }
    }
}
