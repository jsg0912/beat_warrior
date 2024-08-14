using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] Text tt;

    public static Player Instance;
    private Unit playerUnit;
    private Rigidbody2D _rigidbody;
    private Animator _animator;

    private List<ActiveSkillPlayer> skillList;

    private ColliderController colliderController;

    public PLAYERSTATUS status;

    private int direction;
    private int jumpCount;
    private bool isMove;
    private bool isInvincibility;

    private GameObject targetInfo;

    void Start()
    {
        Initialize();
    }

    private void FixedUpdate()
    {
        if (status != PLAYERSTATUS.DEAD) Move();
    }

    void Update()
    {
        if (status != PLAYERSTATUS.DEAD)
        {
            Jump();
            Down();
            Skill();
        }

        if (Input.GetKey(KeyCode.B)) RestartPlayer();
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
            new Skill2(),
            new RecoveryHP()
        };

        foreach (var skill in skillList) skill.Initialize();

        SetPlayerStatus(PLAYERSTATUS.IDLE);

        playerUnit = new Unit(new PlayerInfo("playerName"), new UnitStat(new Dictionary<StatKind, int>{
            {StatKind.HP, PlayerConstant.hpMax},
            {StatKind.ATK, PlayerConstant.atk},
        }));

        direction = 1;
        jumpCount = PlayerConstant.jumpCountMax;
        isMove = true;
        isInvincibility = false;
    }

    public void RestartPlayer()
    {
        Initialize();
    }

    public PLAYERSTATUS GetPlayerStatus()
    {
        return status;
    }

    public void SetPlayerStatus(PLAYERSTATUS status)
    {
        this.status = status;

        _animator.SetBool(PlayerConstant.runAnimBool, status == PLAYERSTATUS.RUN);
        _animator.SetBool(PlayerConstant.jumpAnimBool, status == PLAYERSTATUS.JUMP);

        switch (status)
        {
            case PLAYERSTATUS.IDLE:
                _animator.SetTrigger(PlayerConstant.idleAnimTrigger);
                break;
            case PLAYERSTATUS.ATTACK:
                _animator.SetTrigger(PlayerSkillConstant.attackAnimTrigger);
                break;
            case PLAYERSTATUS.DASH:
                _animator.SetTrigger(PlayerSkillConstant.dashAnimTrigger);
                break;
            case PLAYERSTATUS.MARK:
                _animator.SetTrigger(PlayerSkillConstant.markAnimTrigger);
                break;
            case PLAYERSTATUS.SKILL1:
                _animator.SetTrigger(PlayerSkillConstant.skill1AnimTrigger);
                break;
            case PLAYERSTATUS.SKILL2:
                _animator.SetTrigger(PlayerSkillConstant.skill2AnimTrigger);
                break;
            case PLAYERSTATUS.DEAD:
                _animator.SetTrigger(PlayerConstant.dieAnimTrigger);
                break;
        }

        if (IsUsingSkill() == true) StartCoroutine(UseSkill());
    }

    public int GetDirection()
    {
        return direction;
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

    IEnumerator UseSkill()
    {
        yield return new WaitForSeconds(0.5f);
        if (status != PLAYERSTATUS.DEAD) SetPlayerStatus(PLAYERSTATUS.IDLE);
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

    public int GetHP()
    {
        return playerUnit.GetHP();
    }

    public int GetFinalStat(StatKind statKind)
    {
        return playerUnit.unitStat.GetFinalStat(statKind);
    }

    public void ChangeCurrentHP(int hp)
    {
        playerUnit.unitStat.ChangeCurrentHP(hp);
    }

    public float GetSkillCoolTime(PLAYERSKILLNAME skillName)
    {
        foreach (ActiveSkillPlayer skill in skillList)
        {
            if (skill.skillName == skillName) return skill.GetCoolTime();
        }

        return 0;
    }

    public void SetTarget(GameObject obj)
    {
        Dash dash = FindSkill(PLAYERSKILLNAME.DASH) as Dash;

        dash.SetTarget(obj);
    }

    public void CheckResetSkills(GameObject obj)
    {
        Dash dash = FindSkill(PLAYERSKILLNAME.DASH) as Dash;

        if (dash.GetTarget() != obj) return;

        foreach (ActiveSkillPlayer playerSkill in skillList)
        {
            playerSkill.ResetCoolTime();
        }
    }

    public GameObject GetTargetInfo()
    {
        return targetInfo;
    }

    private void Move()
    {
        if (IsMoveable() == false) return;

        isMove = true;

        if (Input.GetKey(KeySetting.keys[ACTION.RIGHT])) SetDirection(1);
        else if (Input.GetKey(KeySetting.keys[ACTION.LEFT])) SetDirection(-1);
        else isMove = false;

        if (IsUsingSkill() == false && status != PLAYERSTATUS.JUMP) SetPlayerStatus(isMove ? PLAYERSTATUS.RUN : PLAYERSTATUS.IDLE);

        if (isMove == true) transform.position += new Vector3(direction * PlayerConstant.moveSpeed * Time.deltaTime, 0, 0);
    }

    private bool IsMoveable()
    {
        switch (status)
        {
            case PLAYERSTATUS.IDLE:
            case PLAYERSTATUS.RUN:
            case PLAYERSTATUS.JUMP:
            case PLAYERSTATUS.ATTACK:
            case PLAYERSTATUS.MARK:
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
            case PLAYERSTATUS.ATTACK:
            case PLAYERSTATUS.MARK:
            case PLAYERSTATUS.DASH:
            case PLAYERSTATUS.SKILL1:
            case PLAYERSTATUS.SKILL2:
                return true;
            default:
                return false;
        }
    }

    private void Down()
    {
        if (Input.GetKeyDown(KeySetting.keys[ACTION.DOWN]))
        {
            colliderController.PassTile();

            SetPlayerStatus(PLAYERSTATUS.JUMP);
        }
    }

    private void Jump()
    {
        if (IsUsingSkill() == true || jumpCount == 0) return;

        if (Input.GetKeyDown(KeySetting.keys[ACTION.JUMP]))
        {
            SetPlayerStatus(PLAYERSTATUS.JUMP);

            jumpCount--;

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

    public Skill FindSkill(PLAYERSKILLNAME name)
    {
        foreach (var skill in skillList)
        {
            if (skill.skillName == name) return skill;
        }

        return null;
    }

    public void GetDamaged(int dmg)
    {
        if (isInvincibility || GetHP() <= 0) return;

        ChangeCurrentHP(-dmg);

        if (GetHP() <= 0)
        {
            Die();
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

    private void Die()
    {
        SetPlayerStatus(PLAYERSTATUS.DEAD);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject obj = collision.gameObject;

        if (_rigidbody.velocity.y <= 0.5f && (obj.CompareTag("Tile") || obj.CompareTag("Base")))
        {
            _animator.SetBool(PlayerConstant.jumpAnimBool, false);
            if (status == PLAYERSTATUS.JUMP) SetPlayerStatus(PLAYERSTATUS.IDLE);
            jumpCount = PlayerConstant.jumpCountMax;
            return;
        }
    }
}
