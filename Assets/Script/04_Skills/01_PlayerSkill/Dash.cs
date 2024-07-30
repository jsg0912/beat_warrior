using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Dash : Skill
{
    private Rigidbody2D rb;

    private List<GameObject> DashTargetMonster;
    private GameObject TargetMonster;

    private float ghostDelayTime;
    private GameObject GhostPrefab;

    private float ghostDelayTime;
    private float ghostDelayTimeMax;
    private GameObject GhostPrefab;

    private void Start()
    {
        status = PLAYERSTATUS.DASH;

        animTrigger = PlayerSkillConstant.dashkAnimTrigger;

        cooltimeMax = PlayerSkillConstant.dashCoolTimeMax;
        cooltime = 0;

        rb = GetComponent<Rigidbody2D>();
        DashTargetMonster = new List<GameObject>();

        ghostDelayTime = 0;
        ghostDelayTimeMax = PlayerSkillConstant.ghostDelayTimeMax;
        GhostPrefab = Resources.Load("Prefab/Ghost") as GameObject;
    }

    protected override void PlaySkill()
    {
        UpdateKey();
        Ghost();

        if (Input.GetKeyDown(key))
        {
            if (cooltime <= 0) return;

            Player.Instance.SetPlayerStatus(status);
            Player.Instance.SetAnimTrigger(animTrigger);

            SkillMethod();

            Player.Instance.SetPlayerStatus(status);
            Player.Instance.SetPlayerAnim(animTrigger);

            cooltime = 0;
        }
    }

    protected override void UpdateKey()
    {
        key = KeySetting.keys[ACTION.DASH];
    }

    protected override void SkillMethod()
    {
        StartCoroutine(Dashing());
    }

    public void SetTarget(GameObject obj)
    {
        TargetMonster = obj;
        cooltime = cooltimeMax;
    }

    private IEnumerator Dashing()
    {
        rb.gravityScale = 0.0f;
        rb.velocity = Vector3.zero;

        Vector2 start = transform.position;
        Vector2 end = TargetMonster.transform.position;
        int dir = end.x > start.x ? 1 : -1;
        end += new Vector2(dir, 0);

        Player.Instance.direction = end.x > transform.position.x ? 1 : -1;
        transform.localScale = new Vector3(Player.Instance.direction, 1, 1);

        Player.Instance.SetIn(true);

        while (Vector2.Distance(end, transform.position) >= 0.05f)
        {
            transform.position = Vector2.Lerp(transform.position, end, 0.03f);
            yield return null;
        }

        Player.Instance.SetIn(false);
        Player.Instance.SetPlayerStatus(PLAYERSTATUS.IDLE);

        transform.position = end;

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
            if (obj.CompareTag("Monster"))
            {
                // ���� ������
                Debug.Log(obj.name);
            }
        }

        DashTargetMonster.Clear();

        rb.gravityScale = 5.0f;
    }

    private void Ghost()
    {
        if (ghostDelayTime > 0)
        {
            ghostDelayTime -= Time.deltaTime;
            return;
        }

        if (Player.Instance.status != PLAYERSTATUS.DASH) return;

        GameObject ghost = Instantiate(GhostPrefab, transform.position, Quaternion.identity);
        ghostDelayTime = ghostDelayTimeMax;
        Destroy(ghost, 1.0f);
    }
}
