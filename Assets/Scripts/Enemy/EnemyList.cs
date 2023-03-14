using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyList : MonoBehaviour
{
    // sua sau
    public int hp = 30;
    public bool isSlow = false;
    private EnemyTimer slowTimer;
    public static List<EnemyList> enemyList = new List<EnemyList>();
    private void Awake()
    {
        slowTimer = gameObject.AddComponent<EnemyTimer>();
        slowTimer.Duration = 5;
        enemyList.Add(this);
    }
    public static void RemoveEnemy(EnemyList e)
    {
        enemyList.Remove(e);
    }
    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public static implicit operator EnemyList(GameObject v)
    {
        throw new NotImplementedException();
    }
    public static EnemyList GetClosestEnemy(Vector3 position, float maxRange)
    {
        EnemyList closest = null;
        if (enemyList != null)
        {
            foreach (EnemyList enemy in enemyList)
            {
                EnemeEntity enemyEntity = enemy.GetComponent<EnemeEntity>();
                if (enemyEntity.isDeaded() == true)
                {
                    continue;
                }else
                {
                    try
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
                    catch (Exception)
                    {
                    }
                    
                }
                
            }
        }
        return closest;
    }
 }
