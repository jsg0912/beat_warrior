using UnityEngine;

public class Monster : MonoBehaviour
{
    public MonsterName monsterName;
    public MonsterUnit monsterUnit;

    protected Animator _animator;

    private GameObject Target;
    private GameObject TargetPrefab;

    private GameObject Hp;
    private GameObject HpPrefab;

    private GameObject Obj;
    private GameObject SpiritPrefab;

    void Start()
    {
        _animator = GetComponent<Animator>();
        monsterUnit = MonsterList.FindMonster(monsterName);
        monsterUnit.pattern.Initialize(gameObject);

        Hp = transform.GetChild(0).transform.gameObject;

        HpPrefab = Resources.Load("Prefab/MonsterHP") as GameObject;
        TargetPrefab = Resources.Load("Prefab/Target") as GameObject;
        SpiritPrefab = Resources.Load("Prefab/Spirit") as GameObject;

        Vector3 TargetPos = gameObject.transform.position + new Vector3(0, 2.8f, 0);
        Target = GameObject.Instantiate(TargetPrefab, TargetPos, Quaternion.identity);

        Hp.GetComponent<EnemyHp>().SetHp(monsterUnit.GetCurrentHP());

    }

    void Update()
    {
        if (monsterUnit.GetIsAlive() == true)
        {
            ShowUI();
            monsterUnit.pattern.PlayPattern();
        }

    }

    public virtual void GetDamaged(int dmg)
    {
        if (monsterUnit.GetIsAlive() == false) return;

        monsterUnit.ChangeCurrentHP(-dmg);

        if (Player.Instance.HitMonsterFuncList != null) Player.Instance.HitMonsterFuncList(monsterUnit);

        if (monsterUnit.GetIsAlive() == false)
        {
            Die();
            return;
        }

        Hp.GetComponent<EnemyHp>().SetHp(monsterUnit.GetCurrentHP());

        _animator.SetTrigger("hurt");
    }

    protected virtual void Die()
    {
        Player.Instance.CheckResetSkills(this.gameObject);
        Instantiate(SpiritPrefab, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
        _animator.SetTrigger("die");
        Destroy(gameObject, 2.0f);
        Destroy(Target);
        Destroy(Hp);
    }

    protected virtual void ShowUI()
    {
        Obj = Player.Instance.GetTargetInfo();

        if (Obj == gameObject)
        {
            Target.SetActive(true);
        }
        else
        {
            Target.SetActive(false);
        }

        Target.GetComponent<Transform>().position = gameObject.transform.position + new Vector3(0, 2.8f, 0);
        Hp.GetComponent<Transform>().position = gameObject.transform.position + new Vector3(0, -0.5f, 0);

    }
}
