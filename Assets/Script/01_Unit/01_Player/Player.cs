using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public delegate void PlayerCreateDelegate(); // [Code Review - KMJ] No ref? - SDH, 20250106
public class Player : DirectionalGameObject
{
    public static Player Instance;
    public Unit playerUnit { get; private set; }
    public int Hp => GetCurrentStat(StatKind.HP);
    public Sprite sprite => spriteRenderer.sprite;
    private BoxCollider2D _collider;
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private List<ActiveSkillPlayer> skillList;
    private List<Skill> traitList = new();

    public ColliderController colliderController;
    [SerializeField] private PlayerStatus _status;
    public PlayerSkillEffectColor playerSkillEffectColor { get; private set; }
    private PlayerStatus status // 수정 필요하면 SDH에게 문의 - SDH, 20250202
    {
        get { return _status; }
        set
        {
            if (Hp <= 0 && value != PlayerStatus.Dead) return;
            else _status = value;
        }
    }


    private bool isGround;
    private Timer jumpOffsetTimer;
    [SerializeField] private bool isInvincibility;
    private BoxCollider2D tileLeft;
    private BoxCollider2D tileRight;

    public HitMonsterFunc hitMonsterFuncList = null;
    public UseSkillFunc useSKillFuncList = null;
    public ReviveSkillFunc reviveSKillFuncList = null;

    public static void TryCreatePlayer()
    {
        if (Instance == null)
        {
            Instance = FindObjectOfType<Player>();
            if (Instance == null && GameManager.Instance.isInGame)
            {
                CreatePlayer();
            }
        }
    }

    private static void CreatePlayer()
    {
        GameObject player;
        player = Instantiate(Resources.Load(PrefabRouter.PlayerPrefab) as GameObject);
        Instance = player.GetComponent<Player>();
        DontDestroyOnLoad(player);

        player.GetComponent<Player>().Initialize();
    }

    public void RecoverHealthyStatus()
    {
        playerUnit.SetFullStatAll();
        ResetSkillCoolTimeAll();
        SetStatus(PlayerStatus.Normal);

        InitializeRigidBody();
        InitializeAttackCollider();

        SetMovingDirection(PlayerConstant.initDirection);
        isGround = false;
        jumpOffsetTimer = new Timer(0.1f);
        jumpOffsetTimer.SetRemainTimeZero();
        SetInvincibility(false);

        PlayerUIManager.InstanceWithoutCreate?.Initialize();
    }

    private void Initialize()
    {
        StopAllCoroutines();
        // TODO: Alternate real user nickname than "playerName" - SDH, 20241204
        playerUnit = new Unit(new PlayerInfo("playerName"), new UnitStat(PlayerConstant.defaultStat));

        skillList = new List<ActiveSkillPlayer>
        {
            new HolyBlade(gameObject),
            new Mark(gameObject),
            new Dash(gameObject),
            new QSkill(gameObject),
            new ESkill(gameObject)
        };

        traitList.Clear();
        Inventory.Instance.Initialize();

        _collider = GetComponent<BoxCollider2D>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        RecoverHealthyStatus();
    }

    private void InitializeRigidBody()
    {
        SetGravityScale(true);
        PhysicsCalculator.StopRigidBody(_rigidbody);
    }

    public void RestartPlayer()
    {
        Initialize();
        SetAnimTrigger(PlayerConstant.restartAnimTrigger);
    }

    // GET Functions
    public PlayerStatus GetPlayerStatus() { return status; }
    public SkillName[] GetTraits() { return traitList.Where(skill => skill.tier != SkillTier.Common).Select(trait => trait.skillName).ToArray(); }
    public int GetCurrentStat(StatKind statKind) { return playerUnit.unitStat.GetCurrentStat(statKind); }
    public int GetFinalStat(StatKind statKind) { return playerUnit.unitStat.GetFinalStat(statKind); }

    // SET Functions
    public void SetStatus(PlayerStatus status)
    {
        switch (status)
        {
            case PlayerStatus.Normal:
                if (this.status == PlayerStatus.Rest)
                {
                    SetAnimTrigger(PlayerConstant.restartAnimTrigger);
                }
                break;
            case PlayerStatus.Dead:
                SetAnimTrigger(PlayerConstant.dieAnimTrigger);
                break;
        }

        this.status = status;
        PlayerUIManager.InstanceWithoutCreate?.SetPlayerFace(status, Hp);
    }

    public void SetLastSkillColor(PlayerSkillEffectColor color)
    {
        playerSkillEffectColor = color;
    }

    public void SetGravityScale(bool gravity)
    {
        _rigidbody.gravityScale = gravity ? PlayerConstant.gravityScale : 0;
        if (!gravity) PhysicsCalculator.StopRigidBody(_rigidbody);
    }

    public void SetAnimTrigger(string trigger)
    {
        // TODO: 아래 과정이 뭔가뭔가임 - SDH, 20250215
        if (trigger == PlayerConstant.restartAnimTrigger)
        {
            if (status != PlayerStatus.Dead && status != PlayerStatus.Rest) return;
        }
        SoundManager.Instance.PlayPlayerSFX(trigger);
        _animator.SetTrigger(trigger);
    }

    public void SetInvincibility(bool isInvin)
    {
        isInvincibility = isInvin;
    }

    public void PlayerAddForce(Vector2 force, int dir) { _rigidbody.AddForce(force * (int)objectDirection * dir, ForceMode2D.Impulse); }

    public void ForceSetCurrentHp(int hp)
    {
        playerUnit.ForceSetCurrentHP(hp);
        PlayerHpUIController.InstanceWithoutCreate?.UpdateHPUI();
    }

    public bool ChangeCurrentHP(int change)
    {
        bool isAlive = playerUnit.ChangeCurrentHP(change);
        PlayerHpUIController.InstanceWithoutCreate?.UpdateHPUI();
        PlayerUIManager.InstanceWithoutCreate?.SetPlayerFace(status, Hp);
        return isAlive;
    }

    public void SetTarget(GameObject obj)
    {
        Dash dash = HaveSkill(SkillName.Dash) as Dash;

        dash.SetTarget(obj);
    }

    private void SetDead()
    {
        InitializeRigidBody(); // 하지 않으면, 공중에서 공격 중에 맞는 경우 gravity 및 속도 리셋이 안되어 우주로 플레이어가 날아감 - SDH, 20250212
        SetStatus(PlayerStatus.Dead);
    }

    public void TryResetSkillsByMarkKill(GameObject obj)
    {
        Dash dash = HaveSkill(SkillName.Dash) as Dash;

        if (dash.targetMonster != obj) return;

        foreach (SkillName skillName in PlayerSkillConstant.ResetSkillListByMarkKill)
            HaveSkill(skillName).ResetCoolTime();
    }

    public void ResetSkillCoolTimeAll()
    {
        foreach (var skill in skillList)
        {
            skill.ResetCoolTime();
        }

        foreach (var skill in traitList)
        {
            skill.ResetCoolTime();
        }
    }

    // 주의: PlayerLift 같은 곳에서 Player를 자식으로 삼는 경우가 있는데, 그럴 때 맵을 이동하면 Player가 같이 사라지는 문제가 있었음
    public void ResetTransform()
    {
        transform.SetParent(null);
        DontDestroyOnLoad(this);
    }

    public void CheckIsMove()
    {
        if (!IsMoveable()) return;

        bool isMove = false;

        if (Input.GetKey(KeySetting.GetKey(PlayerAction.Right)))
        {
            SetMovingDirection(Direction.Right);
            isMove = true;
        }

        if (Input.GetKey(KeySetting.GetKey(PlayerAction.Left)))
        {
            SetMovingDirection(Direction.Left);
            isMove = true;
        }

        _animator.SetFloat("Speed", isMove ? 1 : 0);

        if (isMove == true) transform.position += new Vector3((int)movingDirection * PlayerConstant.moveSpeed * Time.deltaTime, 0, 0);
    }

    public bool CheckWall()
    {
        float movingDirection = GetMovingDirectionFloat();
        Vector3 start = GetMiddlePos();
        Vector3 dir = Vector3.right * movingDirection;

        RaycastHit2D rayHit = Physics2D.Raycast(start, dir, GetSize().x + 0.05f, LayerMask.GetMask(LayerConstant.Tile));
        //Debug.DrawLine(start, start + dir * 0.05f, Color.red);
        return rayHit.collider != null && rayHit.collider.CompareTag(TagConstant.Base);
    }

    public void CheckPlayerCommand()
    {
        if (IsActionAble())
        {
            TryJump();
            Down();
            Skill();
        }
        FixedSkillUpdate();
        CheckGround();
    }

    public void ForcePlayerAction(PlayerAction playerAction)
    {
        if (IsActionAble())
        {
            switch (playerAction)
            {
                case PlayerAction.Jump:
                    TryJump();
                    break;
                case PlayerAction.Down:
                    Down();
                    break;
                case PlayerAction.Skill1:
                    ForceUseSkill<QSkill>(SkillName.Skill1);
                    break;
                case PlayerAction.Skill2:
                    ForceUseSkill<ESkill>(SkillName.Skill2);
                    break;
                case PlayerAction.Attack:
                    ForceUseSkill<HolyBlade>(SkillName.Attack);
                    break;
                case PlayerAction.Tutorial_Mark:
                    ForceUseSkill<Mark>(SkillName.Mark);
                    break;
                case PlayerAction.Tutorial_Dash:
                    ForceUseSkill<Dash>(SkillName.Dash);
                    break;
            }
        }
    }

    private void ForceUseSkill<T>(SkillName skillName) where T : ActiveSkillPlayer
    {
        (skillList.Find(skill => skill.skillName == skillName) as T).CheckInputKeyCode(true);
    }

    private bool IsMoveable()
    {
        switch (status)
        {
            case PlayerStatus.Normal:
                return true;
            default:
                return false;
        }
    }

    public bool IsActionAble()
    {
        switch (status)
        {
            case PlayerStatus.Normal:
                return true;
            default:
                return false;
        }
    }

    public bool IsUsingSkill()
    {
        switch (status)
        {
            case PlayerStatus.Skill:
                return true;
            default:
                return false;
        }
    }

    private void Down()
    {
        if (!Input.GetKeyDown(KeySetting.GetKey(PlayerAction.Down))) return;
        if (!isGround) return;

        if (tileLeft != null) colliderController.PassTile(tileLeft);
        if (tileRight != null && tileRight != tileLeft) colliderController.PassTile(tileRight);
    }

    public void CheckGround()
    {
        if (jumpOffsetTimer.remainTime > 0) return;

        Vector3 left = GetBottomPos() - new Vector3(GetSize().x / 2, 0, 0);
        Vector3 right = GetBottomPos() + new Vector3(GetSize().x / 2, 0, 0);

        RaycastHit2D rayHitLeft = Physics2D.Raycast(left, Vector3.down, 0.1f, LayerMask.GetMask(LayerConstant.Tile));
        RaycastHit2D rayHitRight = Physics2D.Raycast(right, Vector3.down, 0.1f, LayerMask.GetMask(LayerConstant.Tile));

        isGround = !(rayHitLeft.collider == null && rayHitRight.collider == null) && PhysicsCalculator.IsStoppedSpeedY(_rigidbody);
        _animator.SetBool(PlayerConstant.groundedAnimBool, isGround);

        if (!isGround) return;

        playerUnit.unitStat.ChangeCurrentStat(StatKind.JumpCount, playerUnit.unitStat.GetFinalStat(StatKind.JumpCount));

        if (rayHitLeft.collider != null) tileLeft = rayHitLeft.collider.GetComponent<BoxCollider2D>();
        if (rayHitRight.collider != null) tileRight = rayHitRight.collider.GetComponent<BoxCollider2D>();
    }

    public void TryJump()
    {
        jumpOffsetTimer.Tick();
        if (!IsActionAble() || playerUnit.unitStat.GetCurrentStat(StatKind.JumpCount) == 0) return;

        if (Input.GetKeyDown(KeySetting.GetKey(PlayerAction.Jump)))
        {
            playerUnit.unitStat.ChangeCurrentStat(StatKind.JumpCount, -1);
            Jump();
        }
    }

    private void Jump()
    {
        SetAnimTrigger(PlayerConstant.jumpAnimTrigger);

        PhysicsCalculator.StopRigidBodyY(_rigidbody);
        _rigidbody.AddForce(Vector2.up * PlayerConstant.jumpPower, ForceMode2D.Impulse);
        jumpOffsetTimer.Initialize();
    }

    public void Dashing(Vector2 end, bool changeDir, bool isInvincibility, bool passWall = true)
    {
        StartCoroutine(Dash(end, changeDir, isInvincibility, passWall));
    }

    private IEnumerator Dash(Vector2 end, bool changeDir, bool isInvincibility, bool passWall = true)
    {
        Direction dir = end.x > transform.position.x ? Direction.Right : Direction.Left;

        _animator.SetBool(PlayerConstant.dashEndAnimBool, false);
        SetGravityScale(false);
        SetInvincibility(isInvincibility);
        if (changeDir == true) SetMovingDirection(dir);

        // TODO: 아래의 50은 임시 상수로, 일종의 보정치 개념임, 실험을 하면서 값을 찾고 어떻게 할지 확인해야함 - 신동환, 2024.08.30
        int expectedMoveCount = (int)Math.Ceiling(1 / PlayerSkillConstant.DashSpeed) + 50;
        int moveCount = 0;
        while (Vector2.Distance(end, transform.position) >= 0.05f && moveCount < expectedMoveCount)
        {
            if (!passWall && CheckWall()) break;

            PlayerGhostController.Instance.TryMakeGhost(dir);

            transform.position = Vector2.Lerp(transform.position, end, PlayerSkillConstant.DashSpeed);
            moveCount++;
            yield return null;
        }

        if (passWall == true)
        {
            transform.position = end;
            if (IsWallAtPosition(GetMiddlePos())) transform.position -= new Vector3(DistanceToWall(GetMiddlePos(), (int)dir), 0, 0);
        }

        _animator.SetBool(PlayerConstant.dashEndAnimBool, true);
        SetGravityScale(true);
        SetInvincibility(false);
        if (changeDir == true) SetMovingDirection((Direction)(-1 * (int)dir));
    }

    private bool IsWallAtPosition(Vector2 position)
    {
        return Physics2D.OverlapCircle(position, GetSize().x / 2, LayerMask.GetMask(LayerConstant.Tile));
    }

    private float DistanceToWall(Vector3 pos, float dir)
    {
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector3.right * dir, GetSize().x, LayerMask.GetMask(LayerConstant.Tile));
        return (hit.point.x - pos.x) * dir;
    }

    public Vector3 GetSize() { return ColliderCalculator.GetSizeBoxCollider2D(_collider); }
    public Vector3 GetMiddlePos() { return ColliderCalculator.GetMiddlePosBoxCollider2D(_collider); }
    public Vector3 GetBottomPos() { return ColliderCalculator.GetBottomPosBoxCollider2D(_collider); }

    private void Skill()
    {
        foreach (var skill in skillList) skill.CheckInputKeyCode();
    }

    private void FixedSkillUpdate()
    {
        foreach (var skill in skillList) skill.CheckFixedInputKeyCode();
    }


    public float GetSkillCoolTime(SkillName skillName)
    {
        foreach (ActiveSkillPlayer skill in skillList)
        {
            if (skill.skillName == skillName) return skill.coolTime;
        }

        return 0;
    }

    public Skill HaveSkill(SkillName name)
    {
        foreach (var skill in skillList)
        {
            if (skill.skillName == name) return skill;
        }

        foreach (var skill in traitList)
        {
            if (skill.skillName == name) return skill;
        }

        return null;
    }

    public bool IsEquippedTrait(SkillName name)
    {
        return traitList.Exists(trait => trait.skillName == name);
    }

    public void AddOrRemoveTrait(SkillName name)
    {
        if (!IsEquippedTrait(name)) EquipTrait(name);
        else RemoveTrait(name);
    }

    public void EquipTrait(SkillName name)
    {
        Skill trait = null;
        switch (name)
        {
            case SkillName.AppendMaxHP:
                trait = new AppendMaxHP(this.gameObject);
                break;
            case SkillName.DoubleJump:
                trait = new DoubleJump(this.gameObject);
                break;
            case SkillName.AppendAttack:
                trait = new AppendMaxAttackCount(this.gameObject);
                break;
            case SkillName.Execution:
                trait = new Execution(this.gameObject);
                break;
            case SkillName.KillRecoveryHP:
                trait = new KillRecoveryHP(this.gameObject);
                break;
            case SkillName.SkillReset:
                trait = new SkillReset(this.gameObject);
                break;
            case SkillName.Revive:
                trait = new Revive(this.gameObject);
                break;
        }

        if (trait == null)
        {
            throw new Exception($"Trait 없어서 추가 실패! {name}");
        }

        trait.GetSkill();
        traitList.Add(trait);
        SoundManager.Instance.SFXPlay(SoundList.Instance.altarEquip);
    }

    public void RemoveTraitByIndex(int index)
    {
        SkillName targetSkill = traitList[index].skillName;
        RemoveTrait(targetSkill);
        return;
    }

    public void RemoveTrait(SkillName name)
    {
        foreach (var trait in traitList)
        {
            if (trait.skillName == name)
            {
                traitList.Remove(trait);
                SoundManager.Instance.SFXPlay(SoundList.Instance.altarUnequip);
                trait.RemoveSkill();
                return;
            }
        }
    }

    public void GetDamaged(int monsterAtk, Direction direction)
    {
        if (isInvincibility || status == PlayerStatus.Dead) return;

        int dmg = monsterAtk - GetFinalStat(StatKind.Def);

        bool isAlive = ChangeCurrentHP(-dmg);

        SoundManager.Instance.SFXPlay(SoundList.Instance.playerHit);

        if (!isAlive)
        {
            SetDead();
            if (reviveSKillFuncList != null)
            {
                reviveSKillFuncList();
                SetStatus(PlayerStatus.Normal);
            }
            return;
        }

        StartCoroutine(Invincibility(PlayerConstant.invincibilityTime));

        SetAnimTrigger(PlayerConstant.hurtAnimTrigger);
        KnockBack(direction);
    }

    public void InitializeAttackCollider() { colliderController.InitializeAttackCollider(); }

    private void KnockBack(Direction direction)
    {
        SetStatus(PlayerStatus.Unmovable);

        if (direction == objectDirection) FlipDirection();
        PlayerAddForce(new Vector2(PlayerConstant.knockBackedDistance, 1.0f), -1); // [Code Review - KMJ] 1.0f가 무슨 의미인지 확인 필요 - SDH, 20250217
        StartCoroutine(KnockBacked(PlayerConstant.knockBackedStunTime));
    }

    private IEnumerator KnockBacked(float timer)
    {
        yield return new WaitForSeconds(timer);
        SetStatus(PlayerStatus.Normal);
    }

    private IEnumerator Invincibility(float timer)
    {
        SetInvincibility(true);
        yield return new WaitForSeconds(timer);
        SetInvincibility(false);
    }

    public bool CheckFullEquipTrait()
    {
        return GetTraits().Length == PlayerConstant.MaxAdditionalSkillCount;
    }

    public void AttackAnimationStart()
    {
        SetStatus(PlayerStatus.Skill);
        SetGravityScale(false);
    }

    public void AttackAnimationEnd()
    {
        SetStatus(PlayerStatus.Normal);
        SetGravityScale(true);
        _animator.SetBool(PlayerConstant.isAttackingAnimBool, false);
    }

    public void UnmovableAnimationStart()
    {
        SetStatus(PlayerStatus.Unmovable);
    }

    public void UnmovableAnimationEnd()
    {
        SetStatus(PlayerStatus.Normal);
        SetGravityScale(true);
    }

    public void PlayPlayerReviveSFX0()
    {
        SoundManager.Instance.SFXPlay(SoundList.Instance.playerRevive0);
    }

    public void PlayPlayerReviveSFX1()
    {
        SoundManager.Instance.SFXPlay(SoundList.Instance.playerRevive1);
    }
}
