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

    private List<PlayerSkill> skillList;

    private ColliderController colliderController;

    public PLAYERSTATUS status;

    private bool isInvincibility;
    private int direction;

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

            foreach (var skill in skillList) skill.CheckSkill();
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

        skillList = new List<PlayerSkill>
        {
            new Attack(),
            new Mark(),
            new Dash(),
            new Skill1(),
            new Skill2(),
            new RecoveryHP()
        };

        foreach (var skill in skillList) skill.Initialize();

        status = PLAYERSTATUS.IDLE;
        _animator.SetTrigger("idle");

        playerUnit = new Unit(new PlayerInfo("playerName"), new UnitStat(new Dictionary<StatKind, int>{
            {StatKind.HP, PlayerConstant.hpMax},
            {StatKind.ATK, PlayerConstant.atk},
        }));

        isInvincibility = false;
        direction = 1;
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

        switch (status)
        {
            case PLAYERSTATUS.ATTACK:
            case PLAYERSTATUS.DASH:
            case PLAYERSTATUS.MARK:
            case PLAYERSTATUS.SKILL1:
            case PLAYERSTATUS.SKILL2:
                StartCoroutine(UseSkill());
                break;
            case PLAYERSTATUS.DEAD:
                _animator.SetTrigger("die");
                break;
        }
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
        foreach (PlayerSkill skill in skillList)
        {
            if (skill.skillName == skillName) return skill.GetCooltime();
        }

        return 0;
    }

    public void SetTarget(GameObject obj)
    {
        foreach (PlayerSkill skill in skillList)
        {
            if (skill.skillName == PLAYERSKILLNAME.DASH) (skill as Dash).SetTarget(obj);
        }
    }

    public void CheckResetSkills(GameObject obj)
    {
        foreach (PlayerSkill skill in skillList)
        {
            if (skill.skillName == PLAYERSKILLNAME.DASH)
            {
                DebugConsole.Log("obj.name");
                DebugConsole.Log(obj.name);
                if ((skill as Dash).GetTarget() == obj)
                {
                    DebugConsole.Log("죽은 Monster가 표식 몬스터 맞음");
                    foreach (PlayerSkill playerSkill in skillList)
                    {
                        playerSkill.ResetCoolTime();
                    }
                    break;
                }
            }
        }
    }

    public GameObject GetTargetInfo()
    {
        return targetInfo;
    }

    private void Move()
    {
        _animator.SetBool("isRun", false);

        if (!IsMoveable()) return;

        if (IsSkillUseable() && status != PLAYERSTATUS.JUMP) SetPlayerStatus(PLAYERSTATUS.IDLE);

        if (!Input.GetKey(KeySetting.keys[ACTION.LEFT]) && !Input.GetKey(KeySetting.keys[ACTION.RIGHT])) return;

        if (Input.GetKey(KeySetting.keys[ACTION.LEFT])) SetDirection(-1);

        if (Input.GetKey(KeySetting.keys[ACTION.RIGHT])) SetDirection(1);

        if (IsSkillUseable()) SetPlayerStatus(PLAYERSTATUS.RUN);
        if (!_animator.GetBool("isJump")) _animator.SetBool("isRun", true);

        transform.position += new Vector3(direction * PlayerConstant.moveSpeed * Time.deltaTime, 0, 0);
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
                return false;
        }
    }

    public bool IsSkillUseable()
    {
        switch (status)
        {
            case PLAYERSTATUS.IDLE:
            case PLAYERSTATUS.RUN:
            case PLAYERSTATUS.JUMP:
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
        }
    }

    private void Jump()
    {
        if (status == PLAYERSTATUS.JUMP || status == PLAYERSTATUS.DASH) return;

        if (Input.GetKeyDown(KeySetting.keys[ACTION.JUMP]) && !_animator.GetBool("isJump"))
        {
            status = PLAYERSTATUS.JUMP;
            _animator.SetBool("isJump", true);

            _rigidbody.AddForce(Vector2.up * PlayerConstant.jumpHeight, ForceMode2D.Impulse);
        }
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

        if (_rigidbody.velocity.y <= 0 && (obj.CompareTag("Tile") || obj.CompareTag("Base")))
        {
            _animator.SetBool("isJump", false);
            if (status == PLAYERSTATUS.JUMP) SetPlayerStatus(PLAYERSTATUS.IDLE);
            return;
        }
    }
}
