using System.Collections;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public MonsterName monsterName;
    public MonsterUnit monsterUnit;

    protected Animator _animator;


    [SerializeField] private MonsterHPUI UIHp;
    [SerializeField] private GameObject Target;

    private GameObject Obj;
    private GameObject SoulPrefab;

    void Start()
    {
        _animator = GetComponent<Animator>();
        monsterUnit = MonsterList.FindMonster(monsterName);
        monsterUnit.pattern.Initialize(gameObject);

        SoulPrefab = Resources.Load("Prefab/Soul") as GameObject;

        UIHp.SetHP(monsterUnit.GetCurrentHP());

    }

    void Update()
    {
        if (monsterUnit.GetIsAlive() == true)
        {
            monsterUnit.pattern.PlayPattern();
        }

    }

    public virtual void GetDamaged(int dmg)
    {
        if (monsterUnit.GetIsAlive() == false) return;

        monsterUnit.ChangeCurrentHP(-dmg);

        if (Player.Instance.hitMonsterFuncList != null) Player.Instance.hitMonsterFuncList(monsterUnit);

        UIHp.SetHP(monsterUnit.GetCurrentHP());

        if (monsterUnit.GetIsAlive() == false)
        {
            Die();
            return;
        }

        _animator.SetTrigger("hurt");
    }

    protected virtual void Die()
    {
        Player.Instance.CheckResetSkills(this.gameObject);
        Instantiate(SoulPrefab, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);

        _animator.SetTrigger("die");
        Destroy(gameObject, 2.0f);
    }

    public void SetTarget()
    {
        StartCoroutine(ShowTargetUI());
    }

    protected IEnumerator ShowTargetUI()
    {
        Target.SetActive(true);

        float timer = PlayerSkillConstant.SkillCoolTime[SkillName.Mark];

        while (timer > 0 && monsterUnit.GetIsAlive() == true)
        {
            timer -= Time.deltaTime;
            yield return null;
        }

        Target.SetActive(false);
    }
}
