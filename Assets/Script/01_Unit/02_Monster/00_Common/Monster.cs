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
    private bool isFixedAnimation = false;

    [SerializeField] private MonsterHPUI HpUI;
    [SerializeField] private GameObject Target;
    [SerializeField] private int AnotherHPValue = 0;

    [SerializeField] public GameObject attackCollider;
    [SerializeField] private MonsterBodyCollider monsterBodyCollider;

    void Start()
    {
        _animator = GetComponent<Animator>();
        monsterUnit = MonsterList.FindMonster(monsterName, AnotherHPValue);
        pattern = PatternFactory.GetPatternByPatternName(monsterUnit.patternName);
        pattern.Initialize(this);

        HpUI.SetMaxHP(monsterUnit.GetCurrentHP()); // Customizing HP Code - SDH, 20250119
    }

    void Update()
    {
        pattern?.PlayPattern();
    }

    // TODO: 임시로 애니메이션 함수 구현, 추후 수정 필요 - 김민지 2024.09.11
    public void PlayAnimation(MonsterStatus status)
    {
        if (isFixedAnimation) return;
        switch (status)
        {
            case MonsterStatus.Attack:
                PlayAnimation(MonsterConstant.attackAnimTrigger);
                break;
            case MonsterStatus.AttackCharge:
                PlayAnimation(MonsterConstant.attackChargeAnimTrigger);
                break;
            case MonsterStatus.AttackEnd:
                PlayAnimation(MonsterConstant.attackEndAnimTrigger);
                break;
            case MonsterStatus.Dead:
                PlayAnimation(MonsterConstant.dieAnimTrigger);
                break;
        }
    }

    public void PlayAnimation(string trigger)
    {
        if (isFixedAnimation) return;
        _animator.SetTrigger(trigger);
    }

    public MonsterStatus GetStatus() { return status; }
    public bool GetIsAttacking()
    {
        switch (status)
        {
            case MonsterStatus.Attack:
            case MonsterStatus.AttackCharge:
            case MonsterStatus.AttackEnd:
                return true;
            default:
                return false;
        }
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

    public bool GetIsKnockBackAble() { return monsterUnit.isKnockBackAble; }
    public bool GetIsTackleAble() { return monsterUnit.isTackleAble; }
    public bool GetIsAlive() { return status != MonsterStatus.Dead; }
    public bool CheckIsAlive()
    {
        if (GetCurrentHP() <= 0)
        {
            SetStatus(MonsterStatus.Dead);
            return false;
        }
        return true;
    }
    public int GetCurrentHP() { return monsterUnit.GetCurrentHP(); }
    public int GetCurrentStat(StatKind statKind) { return monsterUnit.unitStat.GetCurrentStat(statKind); }
    public void AttackedByPlayer(int playerATK)
    {
        GetDamaged(playerATK);
        if (Player.Instance.hitMonsterFuncList != null) Player.Instance.hitMonsterFuncList(this); // TODO: 데미지 입기 전, 입은 후, 입히면서 등의 시간 순서에 따라 특성 발동 구분해야 함.
        PlayAnimation(MonsterConstant.hurtAnimTrigger);
    }
    public virtual void GetDamaged(int dmg)
    {
        monsterUnit.ChangeCurrentHP(-dmg);

        HpUI.SetHP(monsterUnit.GetCurrentHP(), monsterUnit.unitStat.GetFinalStat(StatKind.HP));

        if (CheckIsAlive() == false)
        {
            Die();
            return;
        }
    }

    public Vector2 GetSize() { return monsterBodyCollider.GetSize(); }
    public Vector3 GetMiddlePos() { return monsterBodyCollider.GetMiddlePos(); }
    public Vector3 GetBottomPos() { return monsterBodyCollider.GetBottomPos(); }

    public void SetWalkingAnimation(bool isWalk) { _animator.SetBool(MonsterConstant.walkAnimBool, isWalk); }
    public void SetStatus(MonsterStatus status)
    {
        if (status == MonsterStatus.Dead)
        {
            SetIsFixedAnimation(false);
        }
        this.status = status;
    }
    public void SetIsTackleAble(bool isTackleAble) { monsterUnit.isTackleAble = isTackleAble; }
    public void SetIsKnockBackAble(bool isKnockBackAble) { monsterUnit.isKnockBackAble = isKnockBackAble; }
    public void SetIsFixedAnimation(bool isFixedAnimation) { this.isFixedAnimation = isFixedAnimation; }
    protected virtual void Die()
    {
        StopAttack();
        Player.Instance.CheckResetSkills(gameObject);

        monsterUnit.ResetIsKnockBackAble();
        monsterUnit.ResetIsTackleAble();
        SetIsFixedAnimation(false);

        PlayAnimation(MonsterStatus.Dead);

        InGameManager.Instance.CreateSoul(transform.position);
        ChapterManager.Instance.AlarmMonsterKilled(monsterName);

        Destroy(gameObject, 2.0f);
    }

    public void StopAttack()
    {
        if (GetIsAttacking() || GetIsAlive() == false)
        {
            pattern?.StopAttack();
        }
    }

    public void SetTarget()
    {
        StartCoroutine(ShowTargetUI());
    }

    protected IEnumerator ShowTargetUI()
    {
        Util.SetActive(Target, true);

        float timer = PlayerSkillConstant.SkillCoolTime[SkillName.Mark];

        while (timer > 0 && GetIsAlive())
        {
            timer -= Time.deltaTime;
            yield return null;
        }

        Util.SetActive(Target, false);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (GetIsTackleAble())
        {
            GameObject obj = collision.gameObject;
            if (obj.CompareTag(TagConstant.Player))
            {
                Player.Instance.GetDamaged(GetCurrentStat(StatKind.ATK));
            }
        }
    }
}