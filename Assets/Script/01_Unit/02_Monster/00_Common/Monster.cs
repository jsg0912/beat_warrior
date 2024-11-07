using System.Collections;
using UnityEngine;

public class Monster : MonoBehaviour
{
    // Hierarchy 상에서 monster Object의 이름을 정해주면 자동으로 같은 이름의 능력치가 할당 됨 - Tony, 2024.09.11
    public MonsterName monsterName;
    public MonsterUnit monsterUnit;
    public Pattern pattern;

    [SerializeField] protected MonsterStatus status;
    protected Animator _animator;
    protected Direction direction;
    protected bool isMoveable = true;

    [SerializeField] private Transform MonsterSprite;
    [SerializeField] private MonsterHPUI UIHp;
    [SerializeField] private GameObject Target;

    private GameObject SoulPrefab;

    void Start()
    {
        _animator = GetComponent<Animator>();
        monsterUnit = MonsterList.FindMonster(monsterName);
        pattern = PatternFactory.GetPatternByPatternName(monsterUnit.patternName);
        pattern.Initialize(this);

        SoulPrefab = Resources.Load(PrefabRouter.SoulPrefab) as GameObject;

        UIHp.SetMaxHP(monsterUnit.GetCurrentHP());
    }

    void Update()
    {
        if (monsterUnit.GetIsAlive() == true)
        {
            pattern?.PlayPattern();
        }
    }

    // TODO: 임시로 애니메이션 함수 구현, 추후 수정 필요 - 김민지 2024.09.11
    public void PlayAnimation(MonsterStatus status)
    {
        switch (status)
        {
            case MonsterStatus.Attack:
                _animator.SetTrigger(MonsterConstant.attackAnimTrigger);
                break;
            case MonsterStatus.Hurt:
                _animator.SetTrigger(MonsterConstant.hurtAnimTrigger);
                break;
            case MonsterStatus.Dead:
                _animator.SetTrigger(MonsterConstant.dieAnimTrigger);
                break;
        }
    }

    public void SetIsWalking(bool isWalk) { _animator.SetBool(MonsterConstant.walkAnimBool, isWalk); }
    public MonsterStatus GetStatus() { return status; }
    public void SetStatus(MonsterStatus status) { this.status = status; }
    public int GetDirection() { return (int)direction; }
    public bool GetIsMoveable() { return isMoveable; }
    public void SetIsMoveable(bool isMoveable) { this.isMoveable = isMoveable; }

    public void SetDirection(Direction direction)
    {
        this.direction = direction;
        MonsterSprite.localScale = new Vector3((int)direction, 1, 1);
    }

    public void ChangeDirection()
    {
        this.direction = (Direction)(-1 * (int)direction);
        SetDirection(direction);
    }

    public virtual void GetDamaged(int dmg)
    {
        if (monsterUnit.GetIsAlive() == false) return;

        monsterUnit.ChangeCurrentHP(-dmg);

        if (Player.Instance.hitMonsterFuncList != null) Player.Instance.hitMonsterFuncList(monsterUnit);

        UIHp.SetHP(monsterUnit.GetCurrentHP(), monsterUnit.unitStat.GetFinalStat(StatKind.HP));

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

        if (Target != null) Target.SetActive(false);
    }
}
