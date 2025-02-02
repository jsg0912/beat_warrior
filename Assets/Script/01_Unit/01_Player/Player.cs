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
    private BoxCollider2D _collider;
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private List<ActiveSkillPlayer> skillList;
    private List<Skill> traitList = new();

    public ColliderController colliderController;
    [SerializeField] private PlayerStatus status;

    private bool isGround;
    private float jumpDeltaTimer;
    private float jumpTimer;
    private bool isInvincibility;
    private BoxCollider2D tileCollider;

    private GameObject targetInfo;

    public delegate void HitMonsterFunc(Monster monster);
    public delegate void UseSkillFunc(Skill skill);
    public HitMonsterFunc hitMonsterFuncList = null;
    public UseSkillFunc useSKillFuncList = null;

    public static void TryCreatePlayer()
    {
        if (Instance == null)
        {
            Instance = FindObjectOfType<Player>();
            if (Instance == null && GameManager.Instance.isInGame == true)
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
        playerUnit = new Unit(new PlayerInfo("playerName"), new UnitStat(new Dictionary<StatKind, int>{
            {StatKind.HP, PlayerConstant.hpMax},
            {StatKind.ATK, PlayerConstant.atk},
            {StatKind.JumpCount, PlayerConstant.jumpCountMax},
            {StatKind.AttackCount, PlayerSkillConstant.attackCountMax}
        }));

        skillList = new List<ActiveSkillPlayer>
        {
            new HolyBlade(gameObject),
            new Mark(gameObject),
            new Dash(gameObject),
            new Skill1(gameObject),
            new Skill2(gameObject)
        };

        _collider = GetComponent<BoxCollider2D>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        SetStatus(PlayerStatus.Idle);

        SetMovingDirection(direction);
        isGround = false;
        jumpDeltaTimer = 0;
        jumpTimer = 0.1f;
        isInvincibility = false;

        ChangeCurrentHP(playerUnit.unitStat.GetFinalStat(StatKind.HP));
    }

    public void Update()
    {
        if (IsUsingSkill())
        {
            _rigidbody.velocity = Vector2.zero; // TODO: 기획에 따라 스킬 사용 중 멈추는 것이 바뀔 수 있음 - SDH, 20250106
        }
    }

    public void RestartPlayer()
    {
        Initialize();
        _animator.SetTrigger(PlayerConstant.restartAnimTrigger);
        PlayerUIManager.Instance.Initialize();
    }

    // GET Functions
    public PlayerStatus GetPlayerStatus() { return status; }
    public SkillName[] GetTraits() { return traitList.Where(skill => skill.tier != SkillTier.Common).Select(trait => trait.skillName).ToArray(); }
    public int GetCurrentStat(StatKind statKind) { return playerUnit.unitStat.GetCurrentStat(statKind); }
    public int GetFinalStat(StatKind statKind) { return playerUnit.unitStat.GetFinalStat(statKind); }
    public GameObject GetTargetInfo() { return targetInfo; }

    // SET Functions
    public void SetStatus(PlayerStatus status)
    {
        this.status = status;

        _animator.SetBool(PlayerConstant.runAnimBool, status == PlayerStatus.Run);
        _animator.SetFloat("Speed", status == PlayerStatus.Run ? 1 : 0);

        switch (status)
        {
            case PlayerStatus.Jump:
                _animator.SetTrigger(PlayerConstant.jumpAnimTrigger);
                break;
            case PlayerStatus.Attack:
                if (UnityEngine.Random.Range(0, 2) == 0) _animator.SetTrigger(PlayerSkillConstant.attackRAnimTrigger);
                else _animator.SetTrigger(PlayerSkillConstant.attackLAnimTrigger);
                break;
            case PlayerStatus.Dash:
                _animator.SetTrigger(PlayerSkillConstant.dashAnimTrigger);
                break;
            case PlayerStatus.Mark:
                _animator.SetTrigger(PlayerSkillConstant.markAnimTrigger);
                break;
            case PlayerStatus.Skill1:
                if (UnityEngine.Random.Range(0, 2) == 0) _animator.SetTrigger(PlayerSkillConstant.skill1RAnimTrigger);
                else _animator.SetTrigger(PlayerSkillConstant.skill1LAnimTrigger);
                break;
            case PlayerStatus.Skill2:
                _animator.SetTrigger(PlayerSkillConstant.skill2AnimTrigger);
                break;
            case PlayerStatus.Dead:
                KnockBacked();
                _animator.SetTrigger(PlayerConstant.dieAnimTrigger);
                break;
        }
    }

    public void SetGravityScale(bool gravity)
    {
        _rigidbody.gravityScale = gravity ? PlayerConstant.gravityScale : 0;
        if (!gravity) _rigidbody.velocity = Vector3.zero;
    }

    public void SetPlayerAnimTrigger(string trigger)
    {
        _animator.SetTrigger(trigger);
    }

    public void SetInvincibility(bool isInvin)
    {
        isInvincibility = isInvin;
    }

    public void PlayerAddForce(Vector2 force, int dir)
    {
        _rigidbody.AddForce(force * (int)objectDirection * dir, ForceMode2D.Impulse);
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

    public void CheckResetSkills(GameObject obj)
    {
        Dash dash = HaveSkill(SkillName.Dash) as Dash;

        if (dash.GetTarget() != obj) return;

        foreach (ActiveSkillPlayer playerSkill in skillList)
        {
            playerSkill.ResetCoolTime();
        }
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

        if (!IsUsingSkill() && status != PlayerStatus.Jump) SetStatus(isMove ? PlayerStatus.Run : PlayerStatus.Idle);

        if (isMove == true) transform.position += new Vector3((int)movingDirection * PlayerConstant.moveSpeed * Time.deltaTime, 0, 0);
    }

    public void CheckPlayerCommand()
    {
        if (status != PlayerStatus.Dead && !IsUsingSkill())
        {
            Jump();
            CheckGround();
            Down();
            Skill();
        }
    }
    private bool IsMoveable()
    {
        switch (status)
        {
            case PlayerStatus.Idle:
            case PlayerStatus.Run:
            case PlayerStatus.Jump:
            case PlayerStatus.Mark:
                // case PlayerStatus.Attack:
                // case PlayerStatus.Skill1:
                return true;
            default:
                return false;
        }
    }

    public bool IsUsingSkill()
    {
        switch (status)
        {
            case PlayerStatus.Attack:
            case PlayerStatus.Mark:
            case PlayerStatus.Dash:
            case PlayerStatus.Skill1:
            case PlayerStatus.Skill2:
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

        isGround = rayHit.collider != null && _rigidbody.velocity.y >= 0;
        _animator.SetBool(PlayerConstant.groundedAnimBool, isGround);

        if (!isGround) return;

        if (status == PlayerStatus.Jump) SetStatus(PlayerStatus.Idle);
        playerUnit.unitStat.ChangeCurrentStat(StatKind.JumpCount, playerUnit.unitStat.GetFinalStat(StatKind.JumpCount));
        tileCollider = rayHit.collider.GetComponent<BoxCollider2D>();

    }

    private void Jump()
    {
        if (jumpDeltaTimer > 0) jumpDeltaTimer -= Time.deltaTime;
        if (IsUsingSkill() == true || playerUnit.unitStat.GetCurrentStat(StatKind.JumpCount) == 0) return;

        if (Input.GetKeyDown(KeySetting.keys[PlayerAction.Jump]))
        {
            SetStatus(PlayerStatus.Jump);

            playerUnit.unitStat.ChangeCurrentStat(StatKind.JumpCount, -1);

            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0.0f);
            _rigidbody.AddForce(Vector2.up * PlayerConstant.jumpHeight, ForceMode2D.Impulse);

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

        // TODO: 아래의 10은 임시 상수로, 일종의 보정치 개념임, 실험을 하면서 값을 찾고 어떻게 할지 확인해야함 - 신동환, 2024.08.30
        int expectedMoveCount = (int)Math.Ceiling(1 / PlayerSkillConstant.DashSpeed) + 10;
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

    public float GetSkillCoolTime(SkillName skillName)
    {
        foreach (ActiveSkillPlayer skill in skillList)
        {
            if (skill.skillName == skillName) return skill.GetCoolTime();
        }

        return 0;
    }

    public Skill HaveSkill(SkillName name)
    {
        foreach (var skill in skillList)
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

    public void GetDamaged(int dmg)
    {
        if (isInvincibility || status == PlayerStatus.Dead) return;

        bool isAlive = ChangeCurrentHP(-dmg);

        if (!isAlive)
        {
            SetDead();
            return;
        }

        StartCoroutine(Invincibility(PlayerConstant.invincibilityTime));

        _animator.SetTrigger("hurt");
        KnockBacked();
    }

    private void KnockBacked()
    {
        PlayerAddForce(new Vector2(5.0f, 1.0f), -1);
    }

    private IEnumerator Invincibility(float timer)
    {
        isInvincibility = true;
        yield return new WaitForSeconds(timer);
        isInvincibility = false;
    }

    public bool CheckFullEquipTrait()
    {
        return GetTraits().Length == PlayerConstant.MaxAdditionalSkillCount;
    }
}
