using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Bullet : MonoBehaviour
{
    public float slow = 2f;
    public static void Create(Vector3 spawnPosition, EnemyList enemy,int typeTower)
    {
        Transform bulletTransform = null;
        if(typeTower == 1)
        {
            bulletTransform = Instantiate(GameAssets.i.normalBullet, spawnPosition, Quaternion.identity);
        }
        else if (typeTower == 2)
        {
            bulletTransform = Instantiate(GameAssets.i.iceBullet, spawnPosition, Quaternion.identity);
        }
        else
        {
            bulletTransform = Instantiate(GameAssets.i.poisonBullet, spawnPosition, Quaternion.identity);
        }
        Bullet normalBullet = bulletTransform.GetComponent<Bullet>();
        normalBullet.Setup(enemy);
    }
    private EnemyList enemy;
    private void Setup(EnemyList enemy)
    {
        this.enemy = enemy;
    }

    private void Update()
    {
        Vector3 targetPosition = enemy.GetPosition();
        Vector3 moveDir = (targetPosition - transform.position).normalized;
        float moveSpeed = 20f;
        transform.position += moveDir * moveSpeed * Time.deltaTime;
        float anlge = changeAngle(moveDir);
        gameObject.transform.eulerAngles = new Vector3(0, 0, anlge);
        //float destroyDistance = 1f;
        //if(Vector3.Distance(transform.position,targetPosition) < destroyDistance)
        //{
        //    Destroy(gameObject);
        //}
    }

    private float changeAngle(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;
        return n;
    }

    public float getSlow()
    {
        return slow;
    }
}
