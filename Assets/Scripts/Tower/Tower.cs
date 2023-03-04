using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;

public class Tower : MonoBehaviour
{

    private Vector3 shootingPosition;
    private Timer timer;
    private void Awake()
    {
        shootingPosition = transform.Find("ShootingPosition").position;
    }
    void Start()
    {
        timer = gameObject.AddComponent<Timer>();
        timer.Duration = 2f;
        timer.Run();
    }
    void Update()
    {
        if(timer.Finished)
        {
            testEnemy enemy = GetClosestEnemy();

            if (enemy != null)
            {
                NormalBullet.Create(shootingPosition, enemy);
            }
            timer.Run();
        }
    }

    private testEnemy GetClosestEnemy()
    {
        return testEnemy.GetClosestEnemy(transform.position, 60f);
    }
}
