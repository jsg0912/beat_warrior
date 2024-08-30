using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] Text tt;

    public static Player Instance;
    public Unit playerUnit;
    private CapsuleCollider2D _collider;
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private List<ActiveSkillPlayer> skillList;
    private List<Skill> traitList = new();

    private ColliderController colliderController;

    [SerializeField] private PlayerStatus status;

    private int direction;
    private bool isOnBaseTile;
    private bool isInvincibility;

    private GameObject targetInfo;

    public delegate void HitMonsterFunc(MonsterUnit monster);
    public HitMonsterFunc HitMonsterFuncList = null;

    void Start()
    {
        Initialize();
    }

    private void FixedUpdate()
    {
        if (status != PlayerStatus.Dead) CheckMove();
    }

    void Update()
    {
        if (status != PlayerStatus.Dead)
        {
            GroundedCheck();
            Jump();
            Down();
            Skill();
        }

        tt.text = playerUnit.unitStat.GetCurrentStat(StatKind.AttackCount).ToString();

        if (Input.GetKeyDown(KeyCode.B)) RestartPlayer();

        // TODO: 임시 코드(추가특성 장착 및 해제)
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            AddOrRemoveTrait(SkillName.AppendMaxHP);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            AddOrRemoveTrait(SkillName.DoubleJump);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            AddOrRemoveTrait(SkillName.Execution);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            AddOrRemoveTrait(SkillName.KillRecoveryHP);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            AddOrRemoveTrait(SkillName.AppendAttack);
        }
    }

    private void Initialize()
    {
        Instance = this;
        DontDestroyOnLoad(this.gameObject);

        playerUnit = new Unit(new PlayerInfo("playerName"), new UnitStat(new Dictionary<StatKind, int>{
            {StatKind.HP, PlayerConstant.hpMax},
            {StatKind.ATK, PlayerConstant.atk},
            {StatKind.JumpCount, PlayerConstant.jumpCountMax},
            {StatKind.AttackCount, PlayerSkillConstant.attackCountMax}
        }));

        skillList = new List<ActiveSkillPlayer>
        {
            new Attack(this.gameObject),
            new Mark(this.gameObject),
            new Dash(this.gameObject),
            new Skill1(this.gameObject),
            new Skill2(this.gameObject)
        };

        _collider = GetComponent<CapsuleCollider2D>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        colliderController = GetComponent<ColliderController>();

        SetPlayerStatus(PlayerStatus.Idle);

        direction = 1;
        isOnBaseTile = false;
        isInvincibility = false;

        UIManager.Instance.SetAndUpdateHPUI(Player.Instance.GetFinalStat(StatKind.HP));
    }

    public void RestartPlayer()
    {
        Initialize();
        _animator.SetTrigger(PlayerConstant.restartAnimTrigger);
    }

    // GET Functions
    public PlayerStatus GetPlayerStatus() { return status; }
    public int GetDirection() { return direction; }
    public SkillName[] GetTraits() { return traitList.Select(trait => trait.skillName).ToArray(); }
    public int GetCurrentHP() { return playerUnit.GetCurrentHP(); }
    public int GetFinalStat(StatKind statKind) { return playerUnit.unitStat.GetFinalStat(statKind); }
    public GameObject GetTargetInfo() { return targetInfo; }

    // SET Functions
    public void SetPlayerStatus(PlayerStatus status)
    {
        this.status = status;

        _animator.SetBool(PlayerConstant.runAnimBool, status == PlayerStatus.Run);
        _animator.SetBool(PlayerConstant.jumpAnimBool, status == PlayerStatus.Jump);

        switch (status)
        {
            case PlayerStatus.Attack:
                _animator.SetTrigger(PlayerSkillConstant.attackAnimTrigger);
                break;
            case PlayerStatus.Dash:
                _animator.SetTrigger(PlayerSkillConstant.dashAnimTrigger);
                break;
            case PlayerStatus.Mark:
                _animator.SetTrigger(PlayerSkillConstant.markAnimTrigger);
                break;
            case PlayerStatus.Skill1:
                _animator.SetTrigger(PlayerSkillConstant.skill1AnimTrigger);
                break;
            case PlayerStatus.Skill2:
                _animator.SetTrigger(PlayerSkillConstant.skill2AnimTrigger);
                break;
            case PlayerStatus.Dead:
                _animator.SetTrigger(PlayerConstant.dieAnimTrigger);
                break;
        }

        if (IsUsingSkill() == true) StartCoroutine(UseSkill());
    }

    public void SetDirection(int dir)
    {
        direction = dir;
        transform.localScale = new Vector3(direction, 1, 1);
    }

    public void SetGravityScale(bool gravity)
    {
        _rigidbody.gravityScale = gravity ? PlayerConstant.gravityScale : 0;
        if (!gravity) _rigidbody.velocity = Vector3.zero;
    }

    public void SetColliderTrigger(bool isTrigger)
    {
        _collider.isTrigger = isTrigger;
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
        _rigidbody.AddForce(force * direction * dir, ForceMode2D.Impulse);
    }

    public bool ChangeCurrentHP(int hp)
    {
        bool currentHP = playerUnit.ChangeCurrentHP(hp);

        UIManager.Instance.UpdateHPUI();

        return currentHP;
    }

    public void SetTarget(GameObject obj)
    {
        Dash dash = HaveSkill(SkillName.Dash) as Dash;

        dash.SetTarget(obj);
    }

    private void SetDead() { SetPlayerStatus(PlayerStatus.Dead); }

    public void CheckResetSkills(GameObject obj)
    {
        Dash dash = HaveSkill(SkillName.Dash) as Dash;

        if (dash.GetTarget() != obj) return;

        foreach (ActiveSkillPlayer playerSkill in skillList)
        {
            playerSkill.ResetCoolTime();
        }
    }

    private void CheckMove()
    {
        if (IsMoveable() == false) return;

        bool isMove = false;

        if (Input.GetKey(KeySetting.keys[Action.Right]))
        {
            SetDirection(1);
            isMove = true;
        }

        if (Input.GetKey(KeySetting.keys[Action.Left]))
        {
            SetDirection(-1);
            isMove = true;
        }

        if (IsUsingSkill() == false && status != PlayerStatus.Jump) SetPlayerStatus(isMove ? PlayerStatus.Run : PlayerStatus.Idle);

        if (isMove == true) transform.position += new Vector3(direction * PlayerConstant.moveSpeed * Time.deltaTime, 0, 0);
    }

    private bool IsMoveable()
    {
        switch (status)
        {
            case PlayerStatus.Idle:
            case PlayerStatus.Run:
            case PlayerStatus.Jump:
            case PlayerStatus.Attack:
            case PlayerStatus.Mark:
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
        if (isOnBaseTile == true) return;

        if (Input.GetKeyDown(KeySetting.keys[Action.Down]))
        {
            colliderController.PassTile();
        }
    }

    private void Jump()
    {
        if (IsUsingSkill() == true || playerUnit.unitStat.GetCurrentStat(StatKind.JumpCount) == 0) return;

        if (Input.GetKeyDown(KeySetting.keys[Action.Jump]))
        {
            SetPlayerStatus(PlayerStatus.Jump);

            playerUnit.unitStat.ChangeCurrentStat(StatKind.JumpCount, -1);

            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0.0f);
            _rigidbody.AddForce(Vector2.up * PlayerConstant.jumpHeight, ForceMode2D.Impulse);
        }
    }

    public void Dashing(Vector2 end, bool changeDir, bool isInvincibility)
    {
        StartCoroutine(Dash(end, changeDir, isInvincibility));
    }

    private IEnumerator Dash(Vector2 end, bool changeDir, bool isInvincibility)
    {
        int dir = end.x > transform.position.x ? 1 : -1;

        SetColliderTrigger(true);
        SetGravityScale(false);
        SetInvincibility(isInvincibility);
        if (changeDir == true) SetDirection(dir);

        while (Vector2.Distance(end, transform.position) >= 0.05f)
        {
            transform.position = Vector2.Lerp(transform.position, end, 0.03f);
            yield return null;
        }

        transform.position = end;

        SetColliderTrigger(false);
        SetGravityScale(true);
        SetInvincibility(false);
        if (changeDir == true) SetDirection(-dir);
    }

    private void Skill()
    {
        foreach (var skill in skillList) skill.CheckInputKeyCode();
    }

    IEnumerator UseSkill()
    {
        yield return new WaitForSeconds(0.5f);
        if (status != PlayerStatus.Dead) SetPlayerStatus(PlayerStatus.Idle);
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
        if (IsEquippedTrait(name)) EquipTrait(name);
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
        }

        traitList.Add(trait);
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
                trait.RemoveSkill();
                return;
            }
        }
    }

    public void GetDamaged(int dmg)
    {
        if (isInvincibility || playerUnit.GetIsAlive() == false) return;

        bool isAlive = ChangeCurrentHP(-dmg);

        if (isAlive == false)
        {
            SetDead();
            return;
        }

        StartCoroutine(Invincibility(PlayerConstant.invincibilityTime));

        _animator.SetTrigger("hurt");
        PlayerAddForce(new Vector2(5.0f, 1.0f), -1);
    }

    private IEnumerator Invincibility(float timer)
    {
        isInvincibility = true;
        yield return new WaitForSeconds(timer);
        isInvincibility = false;
    }

    private void GroundedCheck()
    {
        bool isGrounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.05f);
        foreach (Collider2D obj in colliders) if (obj.CompareTag("Tile") || obj.CompareTag("Base")) isGrounded = true;

        _animator.SetBool(PlayerConstant.groundedAnimBool, isGrounded);
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject obj = collision.gameObject;

        if (_rigidbody.velocity.y <= 0.5f && (obj.CompareTag("Tile") || obj.CompareTag("Base")))
        {
            if (obj.CompareTag("Base")) isOnBaseTile = true;
            _animator.SetBool(PlayerConstant.jumpAnimBool, false);
            if (status == PlayerStatus.Jump) SetPlayerStatus(PlayerStatus.Idle);
            playerUnit.unitStat.ChangeCurrentStat(StatKind.JumpCount, playerUnit.unitStat.GetFinalStat(StatKind.JumpCount));
            return;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        GameObject obj = collision.gameObject;

        if ((obj.CompareTag("Tile") || obj.CompareTag("Base")))
        {
            if (obj.CompareTag("Base")) isOnBaseTile = false;
        }
    }
}
