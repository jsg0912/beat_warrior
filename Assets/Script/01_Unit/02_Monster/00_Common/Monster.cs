using UnityEngine;

public abstract class Monster : Unit
{
    protected Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        isAlive = true;
        unitStat.hp = 3;
    }

    public virtual void GetDamaged(int dmg)
    {
        unitStat.hp -= dmg;

        if (unitStat.hp <= 0)
        {
            Die();
            return;
        }

        anim.SetTrigger("hurt");
    }

    protected virtual void Die()
    {
        isAlive = false;
        anim.SetTrigger("die");
        Destroy(this.gameObject, 2.0f);
    }
}
