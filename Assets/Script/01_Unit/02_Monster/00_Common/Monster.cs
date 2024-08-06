using System;
using UnityEngine;

public class Monster : MonoBehaviour
{
    protected Animator anim;
    public MonsterName monsterName;
    public MonsterUnit monsterUnit;
    private GameObject HpPrefab;
    private GameObject Hp;
    private Vector3 HpPos;

    void Start()
    {
        anim = GetComponent<Animator>();
        monsterUnit = MonsterList.FindMonster(monsterName);
        monsterUnit.pattern.Initialize(gameObject);

        HpPrefab = Resources.Load("Prefab/EnemyHeart") as GameObject;


        Vector3 HpPos = gameObject.transform.position + new Vector3(0, -0.5f, 0);
        Hp = GameObject.Instantiate(HpPrefab, HpPos, Quaternion.identity);

        Hp.GetComponent<EnemyHp>().SetHp(monsterUnit.unitStat.hp);

    }

    void Update()
    {
        if(monsterUnit.isAlive == true)
        {
            ShowUI();
            monsterUnit.pattern.PlayPattern();
        }
        
    }

    public virtual void GetDamaged(int dmg)
    {
        monsterUnit.unitStat.hp -= dmg;
        Hp.GetComponent<EnemyHp>().SetHp(monsterUnit.unitStat.hp);

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
        anim.SetTrigger("die");
        Destroy(gameObject, 2.0f);
        Destroy(Hp);
        
    }

    protected virtual void ShowUI()
    {
        Hp.GetComponent<Transform>().position = gameObject.transform.position + new Vector3(0, -0.5f, 0);

    }
}
