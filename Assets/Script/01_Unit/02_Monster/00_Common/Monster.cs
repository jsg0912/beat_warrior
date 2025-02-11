using System.Collections;
using UnityEngine;

public class Monster : DirectionalGameObject
{
    // Hierarchy 상에서 monster Object의 이름을 정해주면 자동으로 같은 이름의 능력치가 할당 됨 - Tony, 2024.09.11
    public MonsterName monsterName;
    private MonsterUnit monsterUnit;
    public Pattern pattern;
    public bool isChasing;
    public Timer timer;

    [SerializeField] private MonsterStatus _status;
    [SerializeField]
    private MonsterStatus status // 수정 필요하면 SDH에게 문의 - SDH, 20250202
    {
        get { return _status; }
        set
        {
            if (GetCurrentStat(StatKind.HP) <= 0 && value != MonsterStatus.Dead)
            {
                return;
            }
            else
            {
                _status = value;
            }
        }
    }

    protected Animator _animator;
    private bool isFixedAnimation = false;

    [SerializeField] private MonsterHPUI HpUI;
    [SerializeField] private GameObject Target;
    [SerializeField] private int AnotherHPValue = 0;

    [SerializeField] public GameObject attackCollider;
    [SerializeField] private MonsterBodyCollider monsterBodyCollider;

    void Start()
    {
        isChasing = false;
        _animator = GetComponent<Animator>();
        _animator.SetBool(MonsterConstant.repeatAttackBool, MonsterConstant.RepeatAttack[monsterName]);
        monsterUnit = MonsterList.FindMonster(monsterName, AnotherHPValue);
        pattern = PatternFactory.GetPatternByPatternName(monsterUnit.patternName);
        pattern.Initialize(this);

        HpUI.SetMaxHP(monsterUnit.GetCurrentHP()); // Customizing HP Code - SDH, 20250119

        timer = new Timer();
    }

    void Update()
    {
        pattern?.PlayPattern();
    }

    public void AttackStart() { pattern.AttackStartMethod(); }
    public void AttackEnd() { pattern.AttackEndMethod(); }

    // TODO: 임시로 애니메이션 함수 구현, 추후 수정 필요 - 김민지 2024.09.11
    public void PlayAnimation(MonsterStatus status)
    {
        if (isFixedAnimation) return;
        switch (status)
        {
            case MonsterStatus.Attack:
                PlayAnimation(MonsterConstant.attackAnimTrigger);
                break;
            case MonsterStatus.Dead:
                SetAnimationBool(status, true);
                break;
        }
    }

    public void SetAnimationBool(MonsterStatus status, bool value)
    {
        switch (status)
        {
            case MonsterStatus.Groggy:
                _animator.SetBool(MonsterConstant.groggyBool, value);
                break;
            case MonsterStatus.Dead:
                _animator.SetBool(MonsterConstant.dieAnimBool, value);
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
                case MonsterStatus.Normal:
                    return true;
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
    public int GetFinalStat(StatKind statKind) { return monsterUnit.unitStat.GetFinalStat(statKind); }
    public void SetBuffMultiply(StatKind statKind, int value) { monsterUnit.unitStat.SetBuffMultiply(statKind, value); }
    public void ResetBuffMultiply(StatKind statKind) { monsterUnit.unitStat.ResetBuffMultiply(statKind); }
    public void AttackedByPlayer(int playerATK, bool isAlreadyCheckHitMonsterFunc = false)
    {
        if (!GetIsAlive()) return;
        int damage = playerATK - monsterUnit.unitStat.GetFinalStat(StatKind.Def);
        if (damage <= 0) return;
        GetDamaged(damage);
        if (Player.Instance.hitMonsterFuncList != null && !isAlreadyCheckHitMonsterFunc) Player.Instance.hitMonsterFuncList(this); // TODO: 데미지 입기 전, 입은 후, 입히면서 등의 시간 순서에 따라 특성 발동 구분해야 함.
        PlayAnimation(MonsterConstant.hurtAnimTrigger);
    }

    public virtual void GetDamaged(int dmg)
    {
        monsterUnit.ChangeCurrentHP(-dmg);

        if (HpUI != null) HpUI.SetHP(monsterUnit.GetCurrentHP(), monsterUnit.unitStat.GetFinalStat(StatKind.HP));

        if (!CheckIsAlive())
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
    public void SetIsTackleAble(bool isTackleAble)
    {
        monsterUnit.isTackleAble = isTackleAble;
    }

    public void SetIsKnockBackAble(bool isKnockBackAble) { monsterUnit.isKnockBackAble = isKnockBackAble; }
    public void SetIsFixedAnimation(bool isFixedAnimation) { this.isFixedAnimation = isFixedAnimation; }
    public virtual void Die()
    {
        StopAttack();
        Player.Instance.TryResetSkillsByMarkKill(gameObject);

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
        if (GetIsAttacking() || !GetIsAlive())
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

        timer.Initialize(PlayerSkillConstant.SkillCoolTime[SkillName.Mark]);

        while (timer.Tick() && GetIsAlive())
        {
            yield return null;
        }

        Util.SetActive(Target, false);
    }

    protected override void FlipAdditionalScaleChangeObjects()
    {
        base.FlipAdditionalScaleChangeObjects();
        monsterBodyCollider.TryFlipPolygonCollider();
    }

    public Direction GetRelativeDirectionToPlayer() { return Player.Instance.GetBottomPos().x > GetBottomPos().x ? Direction.Right : Direction.Left; }
    public float GetRelativePlayerDirectionFloat() { return Player.Instance.GetBottomPos().x > GetBottomPos().x ? 1.0f : -1.0f; }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (GetIsTackleAble())
        {
            GameObject obj = collision.gameObject;
            if (obj.CompareTag(TagConstant.Player))
            {
                Player.Instance.GetDamaged(GetFinalStat(StatKind.ATK), GetRelativeDirectionToPlayer());
            }
        }
    }
}