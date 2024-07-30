using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public static Player Instance;
    public Unit playerUnit;
    private Rigidbody2D rb;
    private Animator anim;

    private Mark mark;
    private Dash dash;
    private Skill1 skill1;
    private Skill2 skill2;
    private ColliderController colliderController;

    public PLAYERSTATUS status;

    private bool isInvincibility;
    private int direction;

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
        }
    }

    private void Initialize()
    {
        Instance = this;

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        colliderController = GetComponent<ColliderController>();

        mark = GetComponent<Mark>();
        dash = GetComponent<Dash>();
        skill1 = GetComponent<Skill1>();
        skill2 = GetComponent<Skill2>();

        status = PLAYERSTATUS.IDLE;
        anim.SetTrigger("idle");

        playerUnit = new Unit(new PlayerInfo("playerName"), new UnitStat(PlayerConstant.hpMax, PlayerConstant.atk));

        isInvincibility = false;
        direction = 1;
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
            case PLAYERSTATUS.MARK:
            case PLAYERSTATUS.SKILL1:
            case PLAYERSTATUS.SKILL2:
                StartCoroutine(UseSkill());
                break;
            case PLAYERSTATUS.DEAD:
                anim.SetTrigger("die");
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
        rb.gravityScale = gravity ? PlayerConstant.gravityScale : 0;
        if (!gravity) rb.velocity = Vector3.zero;
    }

        IEnumerator UseSkill()
    {
        yield return new WaitForSeconds(0.5f);
        SetPlayerStatus(PLAYERSTATUS.IDLE);
    }

    public void SetPlayerAnimTrigger(string trigger)
    {
        anim.SetTrigger(trigger);
    }

    public void SetInvincibility(bool isInvin)
    {
        isInvincibility = isInvin;
    }

    public int GetHP()
    {
        return playerUnit.unitStat.hp;
    }

    public float GetMarkCoolTime()
    {
        return mark.GetCooltime();
    }

    public float GetDashCoolTime()
    {
        return dash.GetCooltime();
    }

    public float GetSkill1CoolTime()
    {
        return skill1.GetCooltime();
    }

    public float GetSkill2CoolTime()
    {
        return skill2.GetCooltime();
    }

    public void SetTarget(GameObject obj)
    {
        dash.SetTarget(obj);
    }

    private void Move()
    {
        direction = 0;
        anim.SetBool("isRun", false);

        if (!IsMoveable()) return;

        if (Input.GetKey(KeySetting.keys[ACTION.LEFT])) SetDirection(-1);

        if (Input.GetKey(KeySetting.keys[ACTION.RIGHT])) SetDirection(1);

        SetPlayerStatus(PLAYERSTATUS.IDLE);

        if (direction != 0)
        {
            SetPlayerStatus(PLAYERSTATUS.RUN);
            if (!anim.GetBool("isJump")) anim.SetBool("isRun", true);
        }

        transform.position += new Vector3(direction * PlayerConstant.moveSpeed * Time.deltaTime, 0, 0);
    }

    private bool IsMoveable()
    {
        switch (status)
        {
            case PLAYERSTATUS.IDLE:
            case PLAYERSTATUS.RUN:
            case PLAYERSTATUS.JUMP:
            case PLAYERSTATUS.MARK:
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

        if (Input.GetKeyDown(KeySetting.keys[ACTION.JUMP]) && !anim.GetBool("isJump"))
        {
            status = PLAYERSTATUS.JUMP;
            anim.SetBool("isJump", true);

            rb.AddForce(Vector2.up * PlayerConstant.jumpHeight, ForceMode2D.Impulse);
        }
    }

    public void GetDamaged()
    {
        if (isInvincibility || GetHP() <= 0) return;

        playerUnit.unitStat.hp--;

        if (GetHP() <= 0)
        {
            Die();
            return;
        }

        StartCoroutine(Invincibility(PlayerConstant.invincibilityTime));

        anim.SetTrigger("hurt");
        if (direction == 1) rb.AddForce(new Vector2(-5f, 1f), ForceMode2D.Impulse);
        else rb.AddForce(new Vector2(5f, 1f), ForceMode2D.Impulse);
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

        if (rb.velocity.y <= 0 && (obj.CompareTag("Tile") || obj.CompareTag("Base")))
        {
            anim.SetBool("isJump", false);
            if (status == PLAYERSTATUS.JUMP) SetPlayerStatus(PLAYERSTATUS.IDLE);
            return;
        }
    }
}
