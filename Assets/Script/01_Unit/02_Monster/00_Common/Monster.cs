using UnityEngine;

public abstract class Monster: MonoBehaviour
{
    protected int hp;
    protected bool isAlive;

    protected Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public virtual void GetDamaged(int dmg)
    {
        hp -= dmg;

        if (hp <= 0)
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
