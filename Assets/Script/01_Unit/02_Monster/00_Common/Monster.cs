using UnityEngine;

public class Monster : MonoBehaviour
{
    protected Animator anim;
    public MonsterName monsterName;
    public MonsterUnit monsterUnit;
    private GameObject TargetPrefab;
    private GameObject Target;
    private GameObject HpPrefab;
    private GameObject Hp;
    private Vector3 TargetPos;
    private Vector3 HpPos;
    private GameObject Obj;

    void Start()
    {
        anim = GetComponent<Animator>();
        monsterUnit = MonsterList.FindMonster(monsterName);
        monsterUnit.pattern.Initialize(gameObject);

        TargetPrefab = Resources.Load("Prefab/Target") as GameObject;
        HpPrefab = Resources.Load("Prefab/EnemyHeart") as GameObject;

        Vector3 TargetPos = gameObject.transform.position + new Vector3(0, 2.8f, 0);
        Target = GameObject.Instantiate(TargetPrefab, TargetPos, Quaternion.identity);

        Vector3 HpPos = gameObject.transform.position + new Vector3(0, -0.5f, 0);
        Hp = GameObject.Instantiate(HpPrefab, HpPos, Quaternion.identity);

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

        anim.SetTrigger("hurt");
    }

    protected virtual void Die()
    {
        Player.Instance.CheckResetSkills(this.gameObject);
        anim.SetTrigger("die");
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
