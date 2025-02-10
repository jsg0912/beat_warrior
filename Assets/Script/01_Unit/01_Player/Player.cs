using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public delegate void PlayerCreateDelegate(); // [Code Review - KMJ] No ref? - SDH, 20250106
public class Player : DirectionalGameObject
{
    public static Player Instance;
    public Unit playerUnit;
    public Sprite sprite => spriteRenderer.sprite;
    private BoxCollider2D _collider;
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private List<ActiveSkillPlayer> skillList;
    private List<Skill> traitList = new();

    public ColliderController colliderController;
    [SerializeField] private PlayerStatus _status;
    [SerializeField]
    private PlayerStatus status // 수정 필요하면 SDH에게 문의 - SDH, 20250202
    {
        get { return _status; }
        set
        {
            if (GetCurrentStat(StatKind.HP) <= 0 && value != PlayerStatus.Dead) return;
            else _status = value;
        }
    }

    public int Hp => playerUnit.unitStat.GetCurrentStat(StatKind.HP);

    private bool isGround;
    private float jumpDeltaTimer;
    private float jumpTimer;
    [SerializeField] private bool isInvincibility;
    private BoxCollider2D tileCollider; // [Code Review - KMJ] TODO: 이제 필요없으면 제거 or TileCollider를 왜 가지고 있는지 모르겠음 - SDH, 20250208

    public PlayerGhostController playerGhostController;
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

    private void Initialize(Direction direction = Direction.Left)
    {
        // TODO: Alternate real user nickname than "playerName" - SDH, 20241204
        playerUnit = new Unit(new PlayerInfo("playerName"), new UnitStat(PlayerConstant.defaultStat));

        skillList = new List<ActiveSkillPlayer>
        {
            new HolyBlade(gameObject),
            new Mark(gameObject),
            new Dash(gameObject),
            new Skill1(gameObject),
            new Skill2(gameObject)
        };

        traitList.Clear();
        Inventory.Instance.Initialize();

        _collider = GetComponent<BoxCollider2D>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        SetStatus(PlayerStatus.Normal);

        SetMovingDirection(direction);
        isGround = false;
        jumpDeltaTimer = 0;
        jumpTimer = 0.1f;
        SetInvincibility(false);

        ChangeCurrentHP(playerUnit.unitStat.GetFinalStat(StatKind.HP));
        playerGhostController = new PlayerGhostController();
        InitializeAttackCollider();
    }

    public void RestartPlayer()
    {
        Initialize();
        SetAnimTrigger(PlayerConstant.restartAnimTrigger);
        PlayerUIManager.Instance.Initialize();
    }

    // GET Functions
    public PlayerStatus GetPlayerStatus() { return status; }
    public SkillName[] GetTraits() { return traitList.Where(skill => skill.tier != SkillTier.Common).Select(trait => trait.skillName).ToArray(); }
    public int GetCurrentStat(StatKind statKind) { return playerUnit.unitStat.GetCurrentStat(statKind); }
    public int GetFinalStat(StatKind statKind) { return playerUnit.unitStat.GetFinalStat(statKind); }

    // SET Functions
    public void SetStatus(PlayerStatus status)
    {
        this.status = status;

        switch (status)
        {
            case PlayerStatus.Dead:
                SetAnimTrigger(PlayerConstant.dieAnimTrigger);
                break;
        }
    }

    public void SetGravityScale(bool gravity)
    {
        _rigidbody.gravityScale = gravity ? PlayerConstant.gravityScale : 0;
        if (!gravity) _rigidbody.velocity = Vector3.zero;
    }

    public void SetAnimTrigger(string trigger) { _animator.SetTrigger(trigger); }

    public void SetInvincibility(bool isInvin)
    {
        isInvincibility = isInvin;
    }

    public void PlayerAddForce(Vector2 force, int dir) { _rigidbody.AddForce(force * (int)objectDirection * dir, ForceMode2D.Impulse); }

    public void ForceSetCurrentHp(int hp)
    {
        playerUnit.ForceSetCurrentHP(hp);
        PlayerHpUIController.Instance?.UpdateHPUI();
    }

    public bool ChangeCurrentHP(int hp)
    {
        bool isAlive = playerUnit.ChangeCurrentHP(hp);
        PlayerHpUIController.Instance?.UpdateHPUI();
        return isAlive;
    }

    public void SetTarget(GameObject obj)
    {
        Dash dash = HaveSkill(SkillName.Dash) as Dash;

        dash.SetTarget(obj);
    }

    private void SetDead() { SetStatus(PlayerStatus.Dead); }

    public void TryResetSkillsByMarkKill(GameObject obj)
    {
        Dash dash = HaveSkill(SkillName.Dash) as Dash;

        if (dash.targetMonster != obj) return;

        foreach (SkillName skillName in PlayerSkillConstant.ResetSkillListByMarkKill)
            HaveSkill(skillName).ResetCoolTime();
    }

    public void CheckIsMove()
    {
        if (!IsMoveable()) return;

        bool isMove = false;

        if (Input.GetKey(KeySetting.keys[PlayerAction.Right]))
        {
            SetMovingDirection(Direction.Right);
            isMove = true;
        }

        if (Input.GetKey(KeySetting.keys[PlayerAction.Left]))
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
        Vector3 start = GetMiddlePos() + new Vector3(GetSize().x * movingDirection / 2, 0, 0);
        Vector3 dir = Vector3.right * movingDirection;

        RaycastHit2D rayHit = Physics2D.Raycast(start, dir, 0.05f, LayerMask.GetMask(LayerConstant.Tile));
        Debug.DrawLine(start, start + dir * 0.05f, Color.red);
        Debug.Log(rayHit.collider != null && rayHit.collider.CompareTag(TagConstant.Base));
        return rayHit.collider != null && rayHit.collider.CompareTag(TagConstant.Base);
    }

    public void CheckPlayerCommand()
    {
        if (IsActionAble())
        {
            Jump();
            Down();
            Skill();
        }
        FixedSkillUpdate();
        CheckGround();
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
        if (!Input.GetKeyDown(KeySetting.keys[PlayerAction.Down])) return;
        if (!isGround) return;
        if (tileCollider == null) return;

        colliderController.PassTile(tileCollider);
    }

    private void CheckGround()
    {
        if (jumpDeltaTimer > 0) return;

        Vector3 left = GetBottomPos() - new Vector3(GetSize().x / 2, 0, 0);
        Vector3 right = GetBottomPos() + new Vector3(GetSize().x / 2, 0, 0);

        RaycastHit2D rayHit = Physics2D.Raycast(left, Vector3.down, 0.1f, LayerMask.GetMask(LayerConstant.Tile));
        if (rayHit.collider == null) rayHit = Physics2D.Raycast(right, Vector3.down, 0.1f, LayerMask.GetMask(LayerConstant.Tile));

        isGround = rayHit.collider != null && Util.IsStoppedSpeed(_rigidbody.velocity.y);
        _animator.SetBool(PlayerConstant.groundedAnimBool, isGround);

        if (!isGround) return;

        playerUnit.unitStat.ChangeCurrentStat(StatKind.JumpCount, playerUnit.unitStat.GetFinalStat(StatKind.JumpCount));
        tileCollider = rayHit.collider.GetComponent<BoxCollider2D>();
    }

    private void Jump()
    {
        if (jumpDeltaTimer > 0) jumpDeltaTimer -= Time.deltaTime;
        if (!IsActionAble() || playerUnit.unitStat.GetCurrentStat(StatKind.JumpCount) == 0) return;

        if (Input.GetKeyDown(KeySetting.keys[PlayerAction.Jump]))
        {
            SetAnimTrigger(PlayerConstant.jumpAnimTrigger);

            playerUnit.unitStat.ChangeCurrentStat(StatKind.JumpCount, -1);

            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0.0f);
            _rigidbody.AddForce(Vector2.up * PlayerConstant.jumpPower, ForceMode2D.Impulse);

            jumpDeltaTimer = jumpTimer;
        }
    }

    public void Dashing(Vector2 end, bool changeDir, bool isInvincibility, bool passWall = true)
    {
        StartCoroutine(Dash(end, changeDir, isInvincibility, passWall));
    }

    private IEnumerator Dash(Vector2 end, bool changeDir, bool isInvincibility, bool passWall = true)
    {
        Direction dir = end.x > transform.position.x ? Direction.Right : Direction.Left;

        SetGravityScale(false);
        SetInvincibility(isInvincibility);
        if (changeDir == true) SetMovingDirection(dir);

        // TODO: 아래의 50은 임시 상수로, 일종의 보정치 개념임, 실험을 하면서 값을 찾고 어떻게 할지 확인해야함 - 신동환, 2024.08.30
        int expectedMoveCount = (int)Math.Ceiling(1 / PlayerSkillConstant.DashSpeed) + 50;
        int moveCount = 0;
        while (Vector2.Distance(end, transform.position) >= 0.05f && moveCount < expectedMoveCount)
        {
            if (!passWall)
            {
                float movingDir = GetMovingDirectionFloat();
                Vector3 start = GetMiddlePos() + new Vector3(GetSize().x / 2, 0, 0) * movingDir;
                Vector3 direction = Vector3.right * movingDir;

                RaycastHit2D rayHit = Physics2D.Raycast(start, direction, 0.1f, LayerMask.GetMask(LayerConstant.Tile));
                if (rayHit.collider != null && rayHit.collider.CompareTag(TagConstant.Base)) break;
            }

            playerGhostController.TryMakeGhost(dir);

            transform.position = Vector2.Lerp(transform.position, end, PlayerSkillConstant.DashSpeed);
            moveCount++;
            yield return null;
        }

        if (passWall == true) transform.position = end;

        SetGravityScale(true);
        SetInvincibility(false);
        if (changeDir == true) SetMovingDirection((Direction)(-1 * (int)dir));
    }

    public Vector3 GetSize() { return Util.GetSizeBoxCollider2D(_collider); }
    public Vector3 GetMiddlePos() { return Util.GetMiddlePosBoxCollider2D(_collider); }
    public Vector3 GetBottomPos() { return Util.GetBottomPosBoxCollider2D(_collider); }

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
            throw new Exception("Trait 없어서 추가 실패!");
        }

        trait.GetSkill();
        traitList.Add(trait);
        SoundManager.Instance.PlayEquipSFX();
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
                SoundManager.Instance.PlayEquipSFX();
                trait.RemoveSkill();
                return;
            }
        }
    }

    public void GetDamaged(int monsterAtk, Direction direction)
    {
        DebugConsole.Log("Player Get Damaged with invincibility: " + isInvincibility);
        if (isInvincibility || status == PlayerStatus.Dead) return;

        int dmg = monsterAtk - GetFinalStat(StatKind.Def);

        bool isAlive = ChangeCurrentHP(-dmg);

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
        PlayerAddForce(new Vector2(PlayerConstant.knockBackedDistance, 1.0f), (int)direction);
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
        DebugConsole.Log("Play Player Invincibility");
        yield return new WaitForSeconds(timer);
        SetInvincibility(false);
        DebugConsole.Log("Stop Player Invincibility");
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
}
