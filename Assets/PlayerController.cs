using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Text tt;

    private Rigidbody2D rb;
    private Animator anim;

    private int hp;
    private int attackPoint;
    private int attackPointMax;

    private bool isAlive;
    private bool canMove;
    private bool isDown;
    private bool isCharge;
    private bool isInvincibility;
    private int direction;

    private float moveSpeed;
    private float jumpSpeed;
    private float attackChargeTime;
    private float invincibilityTime;

    private float markCoolTimeMax;
    private float dashCoolTimeMax;
    private float skill1CoolTimeMax;
    private float skill2CoolTimeMax;

    private float markCoolTime;
    private float dashCoolTime;
    private float skill1CoolTime;
    private float skill2CoolTime;

    private GameObject TargetMonster;

    Vector2 target;

    void Start()
    {
        Initialize();
    }

    private void FixedUpdate()
    {
        if (isAlive) Move();
    }

    void Update()
    {
        if (isAlive)
        {
            Jump();
            Down();
            Attack();
            Mark();
            Dash();
            CountCoolTime();

            tt.text = attackPoint + "";
        }
    }

    private void Initialize()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        anim.SetTrigger("idle");

        hp = 0;
        attackPointMax = 2;
        attackPoint = attackPointMax;

        isAlive = true;
        canMove = true;
        isDown = false;
        isCharge = false;
        isInvincibility = false;
        direction = 1;

        moveSpeed = 10.0f;
        jumpSpeed = 20.0f;
        attackChargeTime = 10.0f;
        invincibilityTime = 0.5f;

        markCoolTimeMax = 10.0f;
        dashCoolTimeMax = 10.0f;
        skill1CoolTimeMax = 10.0f;
        skill2CoolTimeMax = 10.0f;

        markCoolTime = 0.0f;
        dashCoolTime = 0.0f;
        skill1CoolTime = 0.0f;
        skill2CoolTime = 0.0f;
    }

    private void CountCoolTime()
    {
        if (markCoolTime >= 0) markCoolTime -= Time.deltaTime;
        if (dashCoolTime >= 0) dashCoolTime -= Time.deltaTime;
        if (skill1CoolTime >= 0) skill1CoolTime -= Time.deltaTime;
        if (skill2CoolTime >= 0) skill2CoolTime -= Time.deltaTime;
    }

    public void ResetSkill()
    {
        markCoolTime = 0.0f;
        dashCoolTime = 0.0f;
        skill1CoolTime = 0.0f;
        skill2CoolTime = 0.0f;

        TargetMonster = null;
    }

    private void Move()
    {
        direction = 0;
        anim.SetBool("isRun", false);

        if (!canMove) return;

        if (Input.GetKey(KeySetting.keys[ACTION.LEFT])) direction = -1;

        if (Input.GetKey(KeySetting.keys[ACTION.RIGHT])) direction = 1;

        if (direction != 0)
        {
            transform.localScale = new Vector3(direction, 1, 1);
            if (!anim.GetBool("isJump")) anim.SetBool("isRun", true);
        }

        transform.position += new Vector3(direction * moveSpeed * Time.deltaTime, 0, 0);
    }

    private void Down()
    {
        if (Input.GetKeyDown(KeySetting.keys[ACTION.DOWN]))
        {
            GetComponent<ColliderController>().PassTile();
        }
    }

    private void Jump()
    {
        if (isDown) return;

        if (Input.GetKeyDown(KeySetting.keys[ACTION.JUMP]) && !anim.GetBool("isJump"))
        {
            anim.SetBool("isJump", true);

            rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
        }
    }

    private void Attack()
    {
        if (attackPoint <= 0) return;

        if (Input.GetKeyDown(KeySetting.keys[ACTION.ATTACK]))
        {
            attackPoint--;
            ChargeAttackPoint();

            anim.SetTrigger("attack");
        }
    }

    private void ChargeAttackPoint()
    {
        if (attackPoint == attackPointMax || isCharge) return;

        StartCoroutine(ChargeTimer());
    }

    private IEnumerator ChargeTimer()
    {
        isCharge = true;

        while (attackPoint < attackPointMax)
        {
            yield return new WaitForSeconds(attackChargeTime);
            attackPoint++;
        }

        isCharge = false;
    }

    private void Mark()
    {
        if (Input.GetKeyDown(KeySetting.keys[ACTION.MARK]))
        {
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(target, Vector2.zero);

            if (hit.collider == null || !hit.collider.gameObject.CompareTag("Monster")) return;

            TargetMonster = hit.collider.gameObject;
            markCoolTime = markCoolTimeMax;
        }
    }

    private void Dash()
    {
        if (Input.GetKeyDown(KeySetting.keys[ACTION.DASH]))
        {
            if (dashCoolTime > 0 || TargetMonster == null) return;

            dashCoolTime = dashCoolTimeMax;

            StartCoroutine(Dashing());
        }
    }

    private IEnumerator Dashing()
    {
        canMove = false;
        rb.gravityScale = 0.0f;

        Vector2 end = TargetMonster.transform.position;

        direction = end.x > transform.position.x ? 1 : -1;
        transform.localScale = new Vector3(direction, 1, 1);
        anim.SetBool("isRun", true);

        while (Vector2.Distance(end, transform.position) >= 0.5f)
        {
            transform.position = Vector2.Lerp(transform.position, end, 0.05f);
            yield return null;
        }

        canMove = true;
        rb.gravityScale = 5.0f;
    }


    private void Skill1()
    {
        if (Input.GetKeyDown(KeySetting.keys[ACTION.SKILL1]))
        {
            skill1CoolTime = skill1CoolTimeMax;
        }
    }

    private void Skill2()
    {
        if (Input.GetKeyDown(KeySetting.keys[ACTION.SKILL2]))
        {
            skill2CoolTime = skill2CoolTimeMax;
        }
    }

    public void GetDamaged()
    {
        if (isInvincibility) return;

        hp--;
        if (hp <= 0) Die();

        StartCoroutine(Invincibility(invincibilityTime));

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
        anim.SetTrigger("die");
        isAlive = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (rb.velocity.y < 0) anim.SetBool("isJump", false);
    }
}
