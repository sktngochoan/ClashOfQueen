using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Bullet : MonoBehaviour
{
    private float slow = 1;
    private float poison;
    
    public void Create(Vector3 spawnPosition, EnemyList enemy,int typeTower,float newSlow,float poison)
    {
        Transform bulletTransform = null;
        if(typeTower == 1)
        {
            bulletTransform = Instantiate(GameAssets.i.normalBullet, spawnPosition, Quaternion.identity);
        }
        else if (typeTower == 2)
        {
            setSlow(newSlow);
            bulletTransform = Instantiate(GameAssets.i.iceBullet, spawnPosition, Quaternion.identity);
        }
        else
        {
            setPoison(poison);
            bulletTransform = Instantiate(GameAssets.i.poisonBullet, spawnPosition, Quaternion.identity);
        }
        Bullet normalBullet = bulletTransform.GetComponent<Bullet>();
        normalBullet.Setup(enemy,newSlow,poison);
    }
    private EnemyList enemy;
    private void Setup(EnemyList enemy,float slow,float poison)
    {
        this.enemy = enemy;
        this.slow = slow;
        this.poison = poison;
    }

    private void Update()
    {
        Vector3 targetPosition = enemy.GetPosition();
        Vector3 moveDir = (targetPosition - transform.position).normalized;
        float moveSpeed = 20f;
        transform.position += moveDir * moveSpeed * Time.deltaTime;
        float anlge = changeAngle(moveDir);
        gameObject.transform.eulerAngles = new Vector3(0, 0, anlge);
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
    public float getPoison()
    {
        return poison;
    }
    public void setSlow(float slow)
    {
        this.slow = slow;
    }
    public void setPoison(float poison)
    {
        this.poison = poison;
    }
}
