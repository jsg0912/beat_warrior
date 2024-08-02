using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Dash : PlayerSkill
{
    private List<GameObject> DashTargetMonster;
    private GameObject TargetMonster;

    private float ghostDelayTime;
    private float ghostDelayTimeMax;
    private GameObject GhostPrefab;

    public override void Initialize()
    {
        skillName = PLAYERSKILLNAME.DASH;
        status = PLAYERSTATUS.DASH;

        animTrigger = PlayerSkillConstant.dashAnimTrigger;

        atk = PlayerSkillConstant.dashAtk;
        cooltimeMax = PlayerSkillConstant.dashCoolTimeMax;
        cooltime = 0;

        DashTargetMonster = new List<GameObject>();

        ghostDelayTime = 0;
        ghostDelayTimeMax = PlayerSkillConstant.ghostDelayTimeMax;
        GhostPrefab = Resources.Load("Prefab/Ghost") as GameObject;
    }

    public override void CheckSkill()
    {
        base.CheckSkill();

        Ghost();
    }

    protected override void UpdateKey()
    {
        key = KeySetting.keys[ACTION.DASH];
    }

    protected override void PlaySkill()
    {
        if (TargetMonster == null) return;

        if (cooltime <= 0) return;

        UseSkill();
    }

    protected override void SkillMethod()
    {
        cooltime = 0;

        Player.Instance.GetComponent<MonoBehaviour>().StartCoroutine(Dashing());
    }

    public void SetTarget(GameObject obj)
    {
        TargetMonster = obj;
        cooltime = cooltimeMax;
    }

    private IEnumerator Dashing()
    {
        Transform playerTransform = Player.Instance.transform;

        Vector2 start = playerTransform.position;
        Vector2 end = TargetMonster.transform.position;

        int dir = end.x > start.x ? 1 : -1;
        end += new Vector2(dir, 0);

        Player.Instance.SetDirection(dir);
        Player.Instance.SetInvincibility(true);
        Player.Instance.SetGravity(false);

        while (Vector2.Distance(end, playerTransform.position) >= 0.05f)
        {
            playerTransform.position = Vector2.Lerp(playerTransform.position, end, 0.03f);
            yield return null;
        }

        playerTransform.position = end;

        Player.Instance.SetDirection(-dir);
        Player.Instance.SetInvincibility(false);
        Player.Instance.SetGravity(true);

        Player.Instance.SetPlayerStatus(PLAYERSTATUS.IDLE);

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
                obj.GetComponent<Monster>().GetDamaged(atk);
            }
        }

        DashTargetMonster.Clear();
    }

    private void Ghost()
    {
        if (ghostDelayTime > 0)
        {
            ghostDelayTime -= Time.deltaTime;
            return;
        }

        if (Player.Instance.status != PLAYERSTATUS.DASH) return;

        GameObject ghost = GameObject.Instantiate(GhostPrefab, Player.Instance.transform.position, Quaternion.identity);
        ghostDelayTime = ghostDelayTimeMax;
        GameObject.Destroy(ghost, 1.0f);
    }
}
