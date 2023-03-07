using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static List<Enemy> enemyList = new List<Enemy>();
    public static Enemy GetClosestEnemy(Vector3 position, float maxRange)
    {
        Enemy closest = null;
        if(enemyList != null)
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
    private void Awake()
    {
        enemyList.Add(this);
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
}
