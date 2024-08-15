using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] Text tt;

    public static Player Instance;
    public Unit playerUnit;
    private Rigidbody2D _rigidbody;
    private Animator _animator;

    private List<ActiveSkillPlayer> skillList;
    private List<Skill> traitList = new List<Skill>();

    private ColliderController colliderController;

    private PlayerStatus status;

    private int direction;
    private int remainJumpCount;
    private bool isMove;
    private bool isInvincibility;

    private GameObject targetInfo;

    void Start()
    {
        Initialize();
    }

    private void FixedUpdate()
    {
        if (status != PlayerStatus.Dead) Move();
    }

    void Update()
    {
        if (status != PlayerStatus.Dead)
        {
            Jump();
            Down();
            Skill();
        }

        if (Input.GetKeyDown(KeyCode.B)) RestartPlayer();

        // TODO: 임시 코드(추가특성 장착 및 해제)
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            AppendMaxHP trait = (AppendMaxHP)traitList.Find(trait => trait.GetType() == typeof(AppendMaxHP));
            if (trait == null)
            {
                trait = new AppendMaxHP();
                traitList.Add(trait);
                trait.GetSkill();
            }
            else
            {
                traitList.Remove(trait);
                trait.RemoveSkill();
            }
        }
    }

    private void Initialize()
    {
        Instance = this;
        DontDestroyOnLoad(this.gameObject);

        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        colliderController = GetComponent<ColliderController>();

        skillList = new List<ActiveSkillPlayer>
        {
            new Attack(),
            new Mark(),
            new Dash(),
            new Skill1(),
            new Skill2()
        };

        foreach (var skill in skillList) skill.Initialize();

        SetPlayerStatus(PlayerStatus.Idle);

        playerUnit = new Unit(new PlayerInfo("playerName"), new UnitStat(new Dictionary<StatKind, int>{
            {StatKind.HP, PlayerConstant.hpMax},
            {StatKind.ATK, PlayerConstant.atk},
            {StatKind.JumpCount, PlayerConstant.jumpCountMax},
            {StatKind.AttackCount, PlayerSkillConstant.attackCountMax}
        }));

        direction = 1;
        remainJumpCount = PlayerConstant.jumpCountMax;
        isMove = true;
        isInvincibility = false;
    }

    public void RestartPlayer()
    {
        Initialize();
    }

    // GET Functions
    public PlayerStatus GetPlayerStatus() { return status; }
    public int GetDirection() { return direction; }
    public bool GetIsFullHP() { return playerUnit.GetIsFUllHP(); }
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
            case PlayerStatus.Idle:
                _animator.SetTrigger(PlayerConstant.idleAnimTrigger);
                break;
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

    public void SetGravity(bool gravity)
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
        _rigidbody.AddForce(force * direction * dir, ForceMode2D.Impulse);
    }

    public bool ChangeCurrentHP(int hp)
    {
        return playerUnit.ChangeCurrentHP(hp);
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

    private void Move()
    {
        if (IsMoveable() == false) return;

        isMove = true;

        if (Input.GetKey(KeySetting.keys[Action.Right])) SetDirection(1);
        else if (Input.GetKey(KeySetting.keys[Action.Left])) SetDirection(-1);
        else isMove = false;

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
                isMove = false;
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
        if (Input.GetKeyDown(KeySetting.keys[Action.Down]))
        {
            colliderController.PassTile();

            SetPlayerStatus(PlayerStatus.Jump);
        }
    }

    private void Jump()
    {
        if (IsUsingSkill() == true || remainJumpCount == 0) return;

        if (Input.GetKeyDown(KeySetting.keys[Action.Jump]))
        {
            SetPlayerStatus(PlayerStatus.Jump);

            remainJumpCount--;

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

        SetGravity(false);
        if (changeDir == true) SetDirection(dir);
        if (isInvincibility == true) SetInvincibility(true);

        while (Vector2.Distance(end, transform.position) >= 0.05f)
        {
            transform.position = Vector2.Lerp(transform.position, end, 0.03f);
            yield return null;
        }

        transform.position = end;

        SetGravity(true);
        if (changeDir == true) SetDirection(-dir);
        if (isInvincibility == true) SetInvincibility(false);
    }

    private void Skill()
    {
        foreach (var skill in skillList) skill.UpdateSkill();
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

    public Skill HaveTrait(SkillName name)
    {
        foreach (var trait in traitList)
        {
            if (trait.skillName == name) return trait;
        }

        return null;
    }

    public void AddTrait(SkillName name)
    {
        Skill trait = null;

        switch (name)
        {
            case SkillName.AppendMaxHP:
                trait = new AppendMaxHP();
                break;
        }

        traitList.Add(trait);
        trait.GetSkill();
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


    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject obj = collision.gameObject;

        if (_rigidbody.velocity.y <= 0.5f && (obj.CompareTag("Tile") || obj.CompareTag("Base")))
        {
            _animator.SetBool(PlayerConstant.jumpAnimBool, false);
            if (status == PlayerStatus.Jump) SetPlayerStatus(PlayerStatus.Idle);
            remainJumpCount = PlayerConstant.jumpCountMax;
            return;
        }
    }
}
