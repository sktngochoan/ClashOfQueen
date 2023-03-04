using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class NormalBullet : MonoBehaviour
{
    public static void Create(Vector3 spawnPosition, testEnemy enemy)
    {
        Transform bulletTransform = Instantiate(GameAssets.i.normalBullet, spawnPosition, Quaternion.identity);
        NormalBullet normalBullet = bulletTransform.GetComponent<NormalBullet>();
        normalBullet.Setup(enemy);
    }
    private testEnemy enemy;
    private void Setup(testEnemy enemy)
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
        float destroyDistance = 1f;
        if(Vector3.Distance(transform.position,targetPosition) < destroyDistance)
        {
            Destroy(gameObject);
        }
    }

    private float changeAngle(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;
        return n;
    }
}
