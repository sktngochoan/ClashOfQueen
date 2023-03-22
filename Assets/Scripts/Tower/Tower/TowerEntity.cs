
using UnityEngine;

public class TowerEntity : MonoBehaviour
{
    [SerializeField]
    private int lvl;
    [SerializeField]
    private float range;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float damage;
    [SerializeField]
    private float poison;
    [SerializeField]
    private float slow;
    [SerializeField]
    private int price;
    [SerializeField]
    private int updatePrice;
    [SerializeField]
    private float damageScale;
    [SerializeField]
    private float speedScale;
    [SerializeField]
    private int rangeScale;
    [SerializeField]
    private float slowScale;
    [SerializeField]
    private float poisonScale;
    [SerializeField]
    private float maxRange;
    private Vector3 position;
    void Start()
    {
        
    }
    public Vector3 getPosition()
    {
        return position;
    }
    public void setPosition( Vector3 position)
    {
        this.position = position;
    }
    public float getRange()
    {
        return range;
    }
    public float getSpeed()
    {
        return speed;
    }
    public float getPoison()
    {
        return poison;
    }
    public float getSlow()
    {
        return slow;
    }
    public float getDamage()
    {
        return damage;
    }
    public int getLv()
    {
        return lvl;
    }
    public int getPrice()
    {
        return price;
    }
    public int getUpdatePrice()
    {
        return updatePrice;
    }
    public void setLv(int lv)
    {
        this.lvl = lv;
    }
    public void setSpeed(float speed)
    {
        this.speed = speed;
    }
    public void setRange(float range)
    {
        this.range = range;
    }
    public void setDamage(float damage)
    {
        this.damage = damage;
    }
    public void setPoison(float poison)
    {
        this.poison = poison;
    }
    public void setSlow(float slow)
    {
        this.slow = slow;
    }
    public void updateTower()
    {
        lvl = lvl + 1;
        damage = damage + (lvl * damageScale);
        if ((lvl - 1) % 3 == 0)
        {
            if(range < maxRange) {
                range = range + 5 * lvl / rangeScale;
            }
            
        }
        if ((lvl - 1) % 5 == 0)
        {
            if(slow != 0)
            {
                slow = 1f + (lvl / 5 * slowScale);
            }
            else if(poison != 0)
            {
                poison = 2f + (1 + lvl / 5 * poisonScale);
            }
            else
            {
                if(speed > 1f)
                {
                    speed = speed - (1.5f * speedScale);
                }
            }
        }
    }
    public void setOnLoad()
    {
        lvl = lvl;
        damage = damage + (lvl * damageScale);
        if (range < maxRange)
        {
           range = 5 + 5 * lvl / rangeScale;
        }
        if ((lvl - 1) % 5 == 0)
        {
            if (slow != 0)
            {
                slow = 1f + (lvl / 5 * slowScale);
            }
            else if (poison != 0)
            {
                poison = 2f + (1 + lvl / 5 * poisonScale);
            }
        }
    }
}
