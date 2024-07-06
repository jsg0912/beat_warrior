using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] Text tt;

    public static Player Instance;

    private Rigidbody2D rb;
    private Animator anim;

    private int hp;
    private int attackPoint;
    private int attackPointMax;

    private bool isAlive;
    private bool canMove;
    private bool isDown;
    private bool isDash;
    private bool isCharge;
    private bool isInvincibility;
    private int direction;

    private float moveSpeed;
    private float jumpSpeed;
    private float markerSpeed;
    private float attackChargeTime;
    private float invincibilityTime;

    private float markCoolTimeMax;
    private float dashCoolTimeMax;
    private float skill1CoolTimeMax;
    private float skill2CoolTimeMax;

    private float ghostDelayTime;
    private float ghostDelayTimeMax;
    private GameObject GhostPrefab;

    private float markCoolTime;
    private float dashCoolTime;
    private float skill1CoolTime;
    private float skill2CoolTime;

    private GameObject MarkerPrefab;
    private GameObject TargetMonster;
    private List<GameObject> DashTargetMonster;

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
            Ghost();
            CountCoolTime();

            tt.text = attackPoint + "";
        }
    }

    private void Initialize()
    {
        Instance = this;

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        anim.SetTrigger("idle");

        hp = 0;
        attackPointMax = 2;
        attackPoint = attackPointMax;

        isAlive = true;
        canMove = true;
        isDown = false;
        isDash = false;
        isCharge = false;
        isInvincibility = false;
        direction = 1;

        moveSpeed = 7.0f;
        jumpSpeed = 20.0f;
        markerSpeed = 30.0f;
        attackChargeTime = 10.0f;
        invincibilityTime = 0.5f;

        markCoolTimeMax = 0f;
        dashCoolTimeMax = 2.0f;
        skill1CoolTimeMax = 2.0f;
        skill2CoolTimeMax = 2.0f;

        ghostDelayTime = 0.0f;
        ghostDelayTimeMax = 0.05f;
        GhostPrefab = Resources.Load("Prefab/Ghost") as GameObject;

        markCoolTime = 0.0f;
        dashCoolTime = 0.0f;
        skill1CoolTime = 0.0f;
        skill2CoolTime = 0.0f;

        MarkerPrefab = Resources.Load("Prefab/Marker") as GameObject;
        DashTargetMonster = new List<GameObject>();
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

    private void Ghost()
    {
        if (ghostDelayTime > 0) ghostDelayTime -= Time.deltaTime;
        else if (!isDash) return;
        else
        {
            GameObject ghost = Instantiate(GhostPrefab, transform.position, Quaternion.identity);
            ghostDelayTime = ghostDelayTimeMax;
            Destroy(ghost, 1.0f);
        }
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
        if (isDown || isDash) return;

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
        if (markCoolTime > 0) return;

        if (Input.GetKeyDown(KeySetting.keys[ACTION.MARK]))
        {
            Vector3 start = transform.position + new Vector3(0, 0.5f, 0);
            Vector3 end = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            GameObject Marker = Instantiate(MarkerPrefab, start, Quaternion.identity);

            Marker.GetComponent<Rigidbody2D>().velocity = (end - start).normalized * markerSpeed;

            markCoolTime = markCoolTimeMax;
        }
    }

    public void SetTarget(GameObject obj)
    {
        TargetMonster = obj;
    }

    private void Dash()
    {
        if (dashCoolTime > 0) return;

        if (Input.GetKeyDown(KeySetting.keys[ACTION.DASH]))
        {
            if (dashCoolTime > 0 || TargetMonster == null) return;

            dashCoolTime = dashCoolTimeMax;
            anim.SetBool("isJump", false);

            StartCoroutine(Dashing());
        }
    }

    private IEnumerator Dashing()
    {
        canMove = false;
        isDash = true;
        rb.gravityScale = 0.0f;
        rb.velocity = Vector3.zero;

        Vector2 start = transform.position;
        Vector2 end = TargetMonster.transform.position;
        int dir = end.x > start.x ? 1 : -1;
        end += new Vector2(dir, 0);

        direction = end.x > transform.position.x ? 1 : -1;
        transform.localScale = new Vector3(direction, 1, 1);
        anim.SetBool("isRun", true);

        while (Vector2.Distance(end, transform.position) >= 0.05f)
        {
            transform.position = Vector2.Lerp(transform.position, end, 0.03f);
            yield return null;
        }

        transform.localScale = new Vector3(-dir, 1, 1);

        Vector2 offset = new Vector2(0, 1.0f);
        RaycastHit2D[] hits;

        hits = Physics2D.RaycastAll(start, end - start, Vector2.Distance(start, end));
        foreach (RaycastHit2D hit in hits) DashTargetMonster.Add(hit.collider.gameObject);

        hits = Physics2D.RaycastAll(start + offset, end - start, Vector2.Distance(start, end));
        foreach (RaycastHit2D hit in hits) DashTargetMonster.Add(hit.collider.gameObject);

        //Debug.DrawRay(start, end - start, Color.red, Vector2.Distance(start, end));
        //Debug.DrawRay(start + offset, end - start, Color.red, Vector2.Distance(start, end));

        DashTargetMonster = DashTargetMonster.Distinct().ToList();

        foreach (GameObject obj in DashTargetMonster)
        {
            if(obj.CompareTag("Monster"))
            {
                // 몬스터 데미지
                Debug.Log(obj.name);
            }
        }

        DashTargetMonster.Clear();

        canMove = true;
        isDash = false;
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

        if (hp <= 0)
        {
            Die();
            return;
        }

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject obj = collision.gameObject;

        if (rb.velocity.y <= 0 && (obj.CompareTag("Tile") || obj.CompareTag("Base")))
        {
            anim.SetBool("isJump", false);
            return;
        }
    }
}
