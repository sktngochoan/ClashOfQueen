using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class testEnemy : MonoBehaviour
{
    public static List<testEnemy> enemyList = new List<testEnemy>();
    public static testEnemy GetClosestEnemy(Vector3 position, float maxRange)
    {
        testEnemy closest = null;
        foreach (testEnemy enemy in enemyList)
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
        return closest;
    }
    private void Awake()
    {
        enemyList.Add(this);
    }
    public Vector3 GetPosition()
    {
        return transform.position;
    }
}
