using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemeEntity : MonoBehaviour
{
    
    Timer slowTimer;
    Timer poisonTimer;
    public bool isSlow = false;
    public bool isPoison = false;
    public bool isDead = false;
    int hp = 5;
    int speed = 5;
    private void Awake()
    {
        EnermyWalk enermyWalk = gameObject.GetComponent<EnermyWalk>();
        enermyWalk.setSpeed(speed);
    }
    void Start()
    {
        slowTimer = gameObject.AddComponent<Timer>();
        slowTimer.Duration = 3;
        poisonTimer = gameObject.AddComponent<Timer>();
        poisonTimer.Duration = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(slowTimer.Finished)
        {
            resetSpeed();
        }
        if(isPoison == true)
        {
            if(poisonTimer.Finished)
            {
                hp = hp - 1;
                poisonTimer.Duration = 1;
                poisonTimer.Run();
            }
        }
        if(hp <= 0)
        {
            isDead = true;
            Destroy(gameObject, 0.5f);
            EnemyList enemy = gameObject.GetComponent<EnemyList>();
            EnemyList.RemoveEnemy(enemy);
        }

    }
    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("iceBullet"))
        {
            if (isSlow == false)
            {
                slowTimer.Duration = 3;
                isSlow = true;
                slowTimer.Run();
                Bullet bullet = coll.gameObject.GetComponent<Bullet>();
                EnermyWalk currentEnemy = gameObject.GetComponent<EnermyWalk>();
                slowEnemy(currentEnemy, bullet.getSlow());
                //slowTimer.OnTimerElapsed += ResetSpeed;
                Destroy(coll.gameObject);
            }
            else
            {
                Destroy(coll.gameObject);
            }
        }
        else if (coll.gameObject.CompareTag("normalBullet"))
        {
            Destroy(coll.gameObject);
        }
        else if (coll.gameObject.CompareTag("poisonBullet"))
        {
            if(isPoison == false)
            {
                isPoison = true;
                poisonTimer.Run();
            }
            Destroy(coll.gameObject);
        }
    }
    private void resetSpeed()
    {
        EnermyWalk currentEnemy = gameObject.GetComponent<EnermyWalk>();
        currentEnemy.setSpeed(5);
        isSlow = false;
    }
    private void slowEnemy(EnermyWalk enemy, float slowSpeed)
    {
        enemy.setSpeed(enemy.getSpeed() - slowSpeed);
    }
    public bool isDeaded()
    {
        return isDead;
    }
}
