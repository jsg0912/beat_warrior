using System.Collections;
using UnityEngine;

public class Monster : DirectionalGameObject
{
    // Hierarchy 상에서 monster Object의 이름을 정해주면 자동으로 같은 이름의 능력치가 할당 됨 - Tony, 2024.09.11
    public MonsterName monsterName;
    private MonsterUnit monsterUnit;
    public Pattern pattern;

    [SerializeField] private MonsterStatus status;
    protected Animator _animator;
    public bool isTackleAble = false; // If it is true, then the monster can tackle the player.

    [SerializeField] private MonsterHPUI HPUI;
    [SerializeField] private GameObject Target;
    [SerializeField] private int AnotherHPValue = 0;

    void Start()
    {
        _animator = GetComponent<Animator>();
        monsterUnit = MonsterList.FindMonster(monsterName, AnotherHPValue);
        pattern = PatternFactory.GetPatternByPatternName(monsterUnit.patternName);
        pattern.Initialize(this);

        HPUI.SetMaxHP(monsterUnit.GetCurrentHP()); // Customizing HP Code - SDH, 20250119
    }

    void Update()
    {
        if (monsterUnit.GetIsAlive() == false)
        {
            StopAttack();
            return;
        }

        pattern?.PlayPattern();
    }

    public void StopAttack() { pattern?.StopAttack(); }

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

    public void PlayAnimation(string trigger) { _animator.SetTrigger(trigger); }

    public void SetWalkingAnimation(bool isWalk) { _animator.SetBool(MonsterConstant.walkAnimBool, isWalk); }
    public MonsterStatus GetStatus() { return status; }
    public bool GetIsAttacking() { return status == MonsterStatus.Attack || status == MonsterStatus.AttackCharge || status == MonsterStatus.AttackEnd; }
    public void SetStatus(MonsterStatus status) { this.status = status; }
    public void SetIsTackleAble(bool isTackleAble)
    {
        Debug.Log("SetIsTackleAble: " + isTackleAble);
        this.isTackleAble = isTackleAble;
    }
    public bool GetIsMoveable()
    {
        {
            switch (status)
            {
                case MonsterStatus.Idle:
                case MonsterStatus.Chase:
                    return true;
                case MonsterStatus.AttackCharge:
                case MonsterStatus.Attack:
                case MonsterStatus.AttackEnd:
                case MonsterStatus.Hurt:
                case MonsterStatus.Dead:
                default:
                    return false;
            }
        }
    }
    public bool GetIsTackleAble() { return isTackleAble; }
    public bool GetIsAlive() { return monsterUnit.GetIsAlive(); }
    public int GetCurrentHP() { return monsterUnit.GetCurrentHP(); }
    public int GetCurrentStat(StatKind statKind) { return monsterUnit.unitStat.GetCurrentStat(statKind); }
    public virtual void GetDamaged(int dmg)
    {
        monsterUnit.ChangeCurrentHP(-dmg);

        if (Player.Instance.hitMonsterFuncList != null) Player.Instance.hitMonsterFuncList(monsterUnit);

        HPUI.SetHP(monsterUnit.GetCurrentHP(), monsterUnit.unitStat.GetFinalStat(StatKind.HP));

        if (monsterUnit.GetIsAlive() == false)
        {
            Die();
            return;
        }

        // GetComponent<Rigidbody2D>().AddForce(new Vector2(-5.0f, 0.5f) * (int)direction, ForceMode2D.Impulse);
        PlayAnimation(MonsterStatus.Hurt);
    }

    protected virtual void Die()
    {
        monsterUnit.SetDead();
        SetStatus(MonsterStatus.Dead);

        Player.Instance.CheckResetSkills(gameObject);

        InGameManager.Instance.CreateSoul(transform.position);

        PlayAnimation(MonsterStatus.Dead);
        Destroy(gameObject, 2.0f);
    }

    public void SetTarget()
    {
        StartCoroutine(ShowTargetUI());
    }

    protected IEnumerator ShowTargetUI()
    {
        Util.SetActive(Target, true);

        float timer = PlayerSkillConstant.SkillCoolTime[SkillName.Mark];

        while (timer > 0 && monsterUnit.GetIsAlive() == true)
        {
            timer -= Time.deltaTime;
            yield return null;
        }

        Util.SetActive(Target, false);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(GetIsTackleAble());
        if (GetIsTackleAble())
        {
            Debug.Log("Tackle");
            GameObject obj = collision.gameObject;
            if (obj.CompareTag(TagConstant.Player))
            {
                Player.Instance.GetDamaged(GetCurrentStat(StatKind.ATK));
            }
        }
    }
}