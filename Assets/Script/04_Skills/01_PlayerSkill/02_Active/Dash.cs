using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Dash : ActiveSkillPlayer
{
    private List<GameObject> DashTargetMonster;
    private GameObject TargetMonster;

    private float ghostDelayTime;
    private float ghostDelayTimeMax;
    private GameObject GhostPrefab;

    public Dash(GameObject unit) : base(unit) { }

    public override void GetSkill()
    {
        skillName = SkillName.Dash;
        status = PlayerStatus.Dash;

        damageMultiplier = PlayerSkillConstant.dashAtk;
        coolTimeMax = PlayerSkillConstant.SkillCoolTime[skillName];
        coolTime = 0;

        DashTargetMonster = new List<GameObject>();

        ghostDelayTime = 0;
        ghostDelayTimeMax = PlayerSkillConstant.ghostDelayTimeMax;
        GhostPrefab = Resources.Load("Prefab/Ghost") as GameObject;
    }

    public override void CheckInputKeyCode()
    {
        base.CheckInputKeyCode();

        Ghost();
    }

    protected override void UpdateKey()
    {
        keyCode = KeySetting.keys[Action.Dash];
    }

    protected override void TrySkill()
    {
        if (TargetMonster == null) return;

        if (coolTime <= 0) return;

        UseSkill();
    }

    protected override void SkillMethod()
    {
        coolTime = 0;

        Transform playerTransform = Player.Instance.transform;

        Vector2 start = playerTransform.position;
        Vector2 end = TargetMonster.transform.position;

        int dir = end.x > start.x ? 1 : -1;
        end += new Vector2(PlayerSkillConstant.DashEndPointInterval * dir, 0);
        Player.Instance.Dashing(end, true, true);

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
                obj.GetComponent<Monster>().GetDamaged(damageMultiplier);
            }
        }

        DashTargetMonster.Clear();
    }

    public void SetTarget(GameObject obj)
    {
        TargetMonster = obj;

        unit.GetComponent<MonoBehaviour>().StartCoroutine(CountCoolTime());
    }

    public GameObject GetTarget()
    {
        return TargetMonster;
    }

    private void Ghost()
    {
        if (ghostDelayTime > 0)
        {
            ghostDelayTime -= Time.deltaTime;
            return;
        }

        if (Player.Instance.GetPlayerStatus() != PlayerStatus.Dash) return;

        GameObject ghost = GameObject.Instantiate(GhostPrefab, Player.Instance.transform.position, Quaternion.identity);
        ghostDelayTime = ghostDelayTimeMax;
        GameObject.Destroy(ghost, 1.0f);
    }
}
