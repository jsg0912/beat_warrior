using System;
using UnityEngine;

public class Monster : MonoBehaviour
{
    protected Animator anim;
    public MonsterName monsterName;
    public MonsterUnit monsterUnit;

    void Start()
    {
        anim = GetComponent<Animator>();
        monsterUnit = MonsterList.FindMonster(monsterName);
        monsterUnit.pattern.Initialize(gameObject);
    }

    void Update()
    {
        if (monsterUnit.isAlive) monsterUnit.pattern.PlayPattern();
    }

    public virtual void GetDamaged(int dmg)
    {
        if (!monsterUnit.isAlive) return;

        monsterUnit.unitStat.hp -= dmg;

        if (monsterUnit.unitStat.hp <= 0)
        {
            Die();
            return;
        }

        anim.SetTrigger("hurt");
    }

    protected virtual void Die()
    {
        monsterUnit.SetDead();
        Player.Instance.CheckResetSkills(this.gameObject);
        anim.SetTrigger("die");
        Destroy(gameObject, 2.0f);
    }
}
