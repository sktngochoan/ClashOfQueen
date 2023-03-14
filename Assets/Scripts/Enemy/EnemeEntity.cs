using UnityEngine;

public class EnemeEntity : MonoBehaviour
{
    
    Timer slowTimer;
    Timer poisonTimer;
    public bool isSlow = false;
    public bool isPoison = false;
    public bool isDead = false;
    private EnermyWalk enermyWalk;
    private float slowPoint;
    private float poisonPoint;
    public float hp;
    public float speed;
    public int coint;
    private GameManager manager;
    private void Awake()
    {
        enermyWalk = gameObject.GetComponent<EnermyWalk>();
        enermyWalk.setSpeed(speed);
    }
    void Start()
    {
        manager = FindObjectOfType<GameManager>();
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
                hp = hp - poisonPoint;
                poisonTimer.Duration = 1;
                poisonTimer.Run();
            }
        }   
        if(hp <= 0)
        {
           
            if(isDead == false)
            {
                manager.coint.addCoint(getCoint());
            }
            isDead = true;
            SpriteRenderer sprite = gameObject.GetComponent<SpriteRenderer>();
            sprite.flipY = true;
            enermyWalk.setSpeed(0);
            Destroy(gameObject, 0.5f);
            EnemyList enemy = gameObject.GetComponent<EnemyList>();
            EnemyList.RemoveEnemy(enemy);
        }

    }
    private void OnCollisionEnter2D(Collision2D coll)
    {
        if(isPoison == true)
        {
            gameObject.GetComponent<Renderer>().material.color = new Color(0f, 1f, 0.1647f);
        }
        if (coll.gameObject.CompareTag("iceBullet"))
        {
            Bullet bullet = coll.gameObject.GetComponent<Bullet>();
            hp -= bullet.getDamage();
            EnermyWalk currentEnemy = gameObject.GetComponent<EnermyWalk>();
            float slow = bullet.getSlow();
            if(slow > 5)
            {
                slow = 5;
            }
            if (isSlow == false)
            {
                slowTimer.Duration = 3;
                isSlow = true;
                slowTimer.Run();
                slowPoint = bullet.getSlow();
                slowEnemy(currentEnemy, bullet.getSlow());
                Destroy(coll.gameObject);
            }
            else
            {
                if(slowPoint < bullet.getSlow())
                {
                    resetSpeed();
                    slowPoint = bullet.getSlow();
                    slowTimer.Duration = 3;
                    isSlow = true;
                    slowTimer.Run();
                    slowEnemy(currentEnemy, bullet.getSlow());
                }
                Destroy(coll.gameObject);
            }
        }
        else if (coll.gameObject.CompareTag("normalBullet"))
        {
            Bullet bullet = coll.gameObject.GetComponent<Bullet>();
            hp -= bullet.getDamage();
            Destroy(coll.gameObject);
        }
        else if (coll.gameObject.CompareTag("poisonBullet"))
        {
            Bullet bullet = coll.gameObject.GetComponent<Bullet>();
            hp -= bullet.getDamage();
            if (isPoison == false)
            {
                poisonPoint = bullet.getPoison();
                gameObject.GetComponent<Renderer>().material.color = new Color(0f, 1f, 0.1647f);
                isPoison = true;
                poisonTimer.Run();
            }
            else
            {
                isPoison = true;
                gameObject.GetComponent<Renderer>().material.color = new Color(0f, 1f, 0.1647f);
                if (poisonPoint < bullet.getPoison())
                {
                    poisonPoint = bullet.getPoison();
                }
            }
            Destroy(coll.gameObject);
        }
    }
    private void resetSpeed()
    {
        EnermyWalk currentEnemy = gameObject.GetComponent<EnermyWalk>();
        currentEnemy.setSpeed(5);
        currentEnemy.GetComponent<Renderer>().material.color = new Color(1f, 1f,1f,1f);
        isSlow = false;
    }
    private void slowEnemy(EnermyWalk enemy, float slowSpeed)
    {
        enemy.GetComponent<Renderer>().material.color = new Color(1f, 0.15f, 0f);
        enemy.setSpeed(enemy.getSpeed() - slowSpeed);
    }
    public bool isDeaded()
    {
        return isDead;
    }
    public float getHp()
    {
        return hp;
    }
    public float getSpeed()
    {
        return speed;
    }
    public int getCoint()
    {
        return coint;
    }
}
