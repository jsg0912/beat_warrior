using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : DirectionalGameObject
{
    // Hierarchy 상에서 monster Object의 이름을 정해주면 자동으로 같은 이름의 능력치가 할당 됨 - Tony, 2024.09.11
    public MonsterName monsterName;
    protected MonsterUnit monsterUnit;
    public Pattern pattern;

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
    [SerializeField] private int AnotherHPValue = 0;
    [SerializeField] private float soulDropRate = ObjectConstant.SoulDropRate;

    protected Animator _animator;
    private bool isFixedAnimation = false;

    [SerializeField] protected MonsterHPUI HpUI;
    [SerializeField] private GameObject attackEffect;
    [SerializeField] private GameObject MarkedEffect;
    [SerializeField] protected List<GameObject> hitEffects = new List<GameObject>();

    [SerializeField] public GameObject attackCollider;
    [SerializeField] protected MonsterBodyCollider monsterBodyCollider;

    private Timer markRemainTimer;

    protected virtual void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.SetBool(MonsterAnimTrigger.repeatAttackBool, MonsterConstant.IsRepeatAttackAnimation[monsterName]);
        monsterUnit = MonsterList.FindMonster(monsterName, AnotherHPValue);
        pattern = PatternFactory.GetPatternByPatternName(monsterUnit.patternName);
        pattern.Initialize(this);

        HpUI.SetMaxHP(monsterUnit.GetCurrentHP()); // Customizing HP Code - SDH, 20250119

        markRemainTimer = new Timer();
    }

    void Update()
    {
        if (GetStatus() == MonsterStatus.Dead) return;
        pattern?.PlayPattern();
    }

    public void AttackStart() { pattern.AttackStartMethod(); }
    public void AttackUpdate() { pattern.AttackUpdateMethod(); }
    public void AttackEnd() { pattern.AttackEndMethod(); }

    public void SetAnimationBool(MonsterStatus status, bool value)
    {
        if (isFixedAnimation) return;
        switch (status)
        {
            case MonsterStatus.Groggy:
                _animator.SetBool(MonsterAnimTrigger.groggyBool, value);
                break;
            case MonsterStatus.Dead:
                _animator.SetBool(MonsterAnimTrigger.dieAnimBool, value);
                break;
        }
    }

    public void SetAnimationFloat(string trigger, float value)
    {
        _animator.SetFloat(trigger, value);
    }

    public void PlayAnimation(string trigger)
    {
        if (isFixedAnimation) return;
        if (trigger == MonsterAnimTrigger.hurtAnimTrigger)
        {
            SetMovingDirection(Player.Instance.GetBottomPos().x > transform.position.x ? Direction.Right : Direction.Left);
            AttackEnd();
        }
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

    public bool GetIsRecognizing()
    {
        switch (status)
        {
            case MonsterStatus.Attack:
            case MonsterStatus.Chase:
                return true;
            default:
                return false;
        }
    }

    public bool GetIsAttackAble()
    {
        switch (status)
        {
            case MonsterStatus.Attack:
            case MonsterStatus.Groggy:
                return false;
            default:
                return true;
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
                default:
                    return false;
            }
        }
    }

    public bool GetIsKnockBackAble() { return monsterUnit.isKnockBackAble; }
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
        if (monsterUnit.unitStat.GetFinalStat(StatKind.Def) > 0) SoundManager.Instance.SFXPlay(SoundList.Instance.defMonsterHit);
        if (damage <= 0) return;

        GetDamaged(damage);
        if (Player.Instance.hitMonsterFuncList != null && !isAlreadyCheckHitMonsterFunc) Player.Instance.hitMonsterFuncList(this); // TODO: 데미지 입기 전, 입은 후, 입히면서 등의 시간 순서에 따라 특성 발동 구분해야 함.

        PlayAnimation(MonsterAnimTrigger.hurtAnimTrigger);
        SoundManager.Instance.SFXPlay(SoundList.Instance.monsterHit);
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

    //TODO: 현재 거의 공격 후 Effect로 쓰는데, 발동 시점이나 네이밍에 문제가 생기면 수정이 필요함.
    public void playAfterAttackEffect()
    {
        if (attackEffect != null) StartCoroutine(Util.PlayInstantEffect(attackEffect, 0.3f));
    }

    public virtual void PlayScarEffect()
    {
        foreach (GameObject hitEffect in hitEffects)
        {
            StartCoroutine(Util.PlayInstantEffect(hitEffect, 0.3f));
        }
    }

    public Vector2 GetSize() { return monsterBodyCollider.GetSize(); }
    public Vector3 GetMiddlePos() { return monsterBodyCollider.GetMiddlePos(); }
    public Vector3 GetBottomPos() { return monsterBodyCollider.GetBottomPos(); }

    public void SetWalkingAnimation(bool isWalk) { _animator.SetBool(MonsterAnimTrigger.walkAnimBool, isWalk); }
    public void SetStatus(MonsterStatus newStatus)
    {
        if (newStatus == MonsterStatus.Dead)
        {
            SetIsFixedAnimation(false);
        }
        status = newStatus;
    }

    public void SetIsKnockBackAble(bool isKnockBackAble)
    {
        if (isKnockBackAble)
        {
            monsterUnit.ResetIsKnockBackAble();
        }
        else monsterUnit.isKnockBackAble = isKnockBackAble;
    }

    public void SetIsFixedAnimation(bool isFixedAnimation)
    {
        if (!GetIsAlive()) return; // 죽은 상태라면 함부로 못바꿈
        this.isFixedAnimation = isFixedAnimation;
    }

    public virtual void Die()
    {
        StopAttack();
        Player.Instance.TryResetSkillsByMarkKill(gameObject);
        Util.SetActive(MarkedEffect, false);

        monsterUnit.ResetIsKnockBackAble();
        SetIsFixedAnimation(false);

        SetAnimationBool(MonsterStatus.Dead, true);
    }

    public void MakePlayerRewards()
    {
        InGameManager.Instance.CreateSoul(transform.position, soulDropRate);
        ChapterManager.Instance.AlarmMonsterKilled(monsterName);
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
        Util.SetActive(MarkedEffect, true);

        markRemainTimer.Initialize(PlayerSkillConstant.SkillCoolTime[SkillName.Mark]);

        while (markRemainTimer.Tick() && GetIsAlive())
        {
            yield return null;
        }

        Util.SetActive(MarkedEffect, false);
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
        GameObject obj = collision.gameObject;
        if (obj.CompareTag(TagConstant.Player))
        {
            Player.Instance.GetDamaged(GetFinalStat(StatKind.ATK), GetRelativeDirectionToPlayer());
        }
    }

    public void PlayMonsterAttackSFX()
    {
        switch (monsterName)
        {
            case MonsterName.Ippali:
                SoundManager.Instance.SFXPlay(SoundList.Instance.monsterIppaliAttack);
                break;
            case MonsterName.Ibkkugi:
                SoundManager.Instance.SFXPlay(SoundList.Instance.monsterIbkkugiAttack);
                break;
            case MonsterName.Koppulso:
                SoundManager.Instance.SFXPlay(SoundList.Instance.monsterKoppulsoAttack);
                break;
            case MonsterName.Dulduli:
                SoundManager.Instance.SFXPlay(SoundList.Instance.monsterDulduliAttack);
                break;
            case MonsterName.Giljjugi:
                SoundManager.Instance.SFXPlay(SoundList.Instance.monsterGiljjugiAttack);
                break;
            case MonsterName.Itmomi:
                SoundManager.Instance.SFXPlay(SoundList.Instance.monsterItmomiAttack);
                break;
        }
    }

    public void PlayMonsterChargeSFX()
    {
        switch (monsterName)
        {
            case MonsterName.Koppulso:
                SoundManager.Instance.SFXPlay(SoundList.Instance.monsterKoppulsoCharge);
                break;
            case MonsterName.Dulduli:
                SoundManager.Instance.SFXPlay(SoundList.Instance.monsterDulduliCharge);
                break;
        }
    }

    public void PlayMonsterThornSFX()
    {
        SoundManager.Instance.SFXPlay(SoundList.Instance.monsterItmomiThorn);
    }

    public void ForceKill()
    {
        SetBuffMultiply(StatKind.Def, -1);
        AttackedByPlayer(GetCurrentHP(), true);
    }
}