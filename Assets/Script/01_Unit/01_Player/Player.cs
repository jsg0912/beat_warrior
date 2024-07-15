using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    private Rigidbody2D rb;
    private Animator anim;

    private Mark mark;
    private Dash dash;
    private Skill1 skill1;
    private Skill2 skill2;
    private ColliderController colliderController;

    public PLAYERSTATUS status;
    private int hp;

    private bool isInvincibility;
    public int direction;

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

        hp = PlayerConstant.hpMax;
        
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

            case PLAYERSTATUS.MARK:
                // anim.SetTrigger("mark");
                this.status = PLAYERSTATUS.IDLE;
                break;
            case PLAYERSTATUS.ATTACK:
                anim.SetTrigger("attack");
                this.status = PLAYERSTATUS.IDLE;
                break;
            case PLAYERSTATUS.SKILL1:
                anim.SetTrigger("skill1");
                this.status = PLAYERSTATUS.IDLE;
                break;
            case PLAYERSTATUS.SKILL2:
                anim.SetTrigger("skill2");
                this.status = PLAYERSTATUS.IDLE;
                break;
            case PLAYERSTATUS.DEAD:
                anim.SetTrigger("die");
                break;
        }
    }
    public void SetIn(bool bb)
    {
        isInvincibility = bb;
    }

    public int GetHP()
    {
        return hp;
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

        if (IsMoveable()) return;

        if (Input.GetKey(KeySetting.keys[ACTION.LEFT])) direction = -1;

        if (Input.GetKey(KeySetting.keys[ACTION.RIGHT])) direction = 1;

        SetPlayerStatus(PLAYERSTATUS.IDLE);

        if (direction != 0)
        {
            SetPlayerStatus(PLAYERSTATUS.RUN);
            transform.localScale = new Vector3(direction, 1, 1);
            if (!anim.GetBool("isJump")) anim.SetBool("isRun", true);
        }

        transform.position += new Vector3(direction * PlayerConstant.moveSpeed * Time.deltaTime, 0, 0);
    }

    private bool IsMoveable()
    {
        return status != PLAYERSTATUS.IDLE && status != PLAYERSTATUS.RUN && status != PLAYERSTATUS.JUMP && status != PLAYERSTATUS.DASH;
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
        if (isInvincibility || hp <= 0) return;

        hp--;

        if (hp <= 0)
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
            SetPlayerStatus(PLAYERSTATUS.IDLE);
            return;
        }
    }
}
