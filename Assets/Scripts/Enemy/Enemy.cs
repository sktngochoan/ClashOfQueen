using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // sua sau
    public int hp = 30;
    public bool isSlow = false;
    private EnemyTimer slowTimer;
    public static List<Enemy> enemyList = new List<Enemy>();
    private void Awake()
    {
        slowTimer = gameObject.AddComponent<EnemyTimer>();
        slowTimer.Duration = 5;
        enemyList.Add(this);
    }
    private void Update()
    {
    }
    public static void RemoveEnemy(Enemy e)
    {
        enemyList.Remove(e);
    }
    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public static implicit operator Enemy(GameObject v)
    {
        throw new NotImplementedException();
    }
    public static Enemy GetClosestEnemy(Vector3 position, float maxRange)
    {
        Enemy closest = null;
        if (enemyList != null)
        {
            foreach (Enemy enemy in enemyList)
            {
                if (Vector3.Distance(position, enemy.GetPosition()) <= maxRange)
                {
                    if (closest == null)
                    {
                        closest = enemy;
                    }
                    else
                    {
                        if (Vector3.Distance(position, enemy.GetPosition()) < Vector3.Distance(position, closest.GetPosition()))
                        {
                            closest = enemy;
                        }
                    }
                }
            }
        }
        return closest;
    }
    private EnermyWalk currentEnemy;
    private void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.CompareTag("iceBullet"))
        {
            if(isSlow == false)
            {
                isSlow = true;
                slowTimer.Duration = 5;
                slowTimer.Run();
                Destroy(coll.gameObject);
                Bullet bullet = coll.gameObject.GetComponent<Bullet>();
                currentEnemy = gameObject.GetComponent<EnermyWalk>();
                slowEnemy(currentEnemy, bullet.getSlow());
                slowTimer.OnTimerElapsed += ResetSpeed;
            }
            else
            {
                slowTimer.Reset();
                Debug.Log(1);
                Destroy(coll.gameObject);
            }
        }
    }
    private void ResetSpeed(object sender, EventArgs e)
    {
        currentEnemy.setSpeed(5);
        isSlow = false;
    }
    private void slowEnemy(EnermyWalk enemy, float slowSpeed)
    {
        enemy.setSpeed(enemy.getSpeed() - slowSpeed);
    }
 }
