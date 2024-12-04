using System.Collections;
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

    public Dash(GameObject unit) : base(unit)
    {
        skillName = SkillName.Dash;
        status = PlayerStatus.Dash;

        damageMultiplier = PlayerSkillConstant.dashAtk;
        coolTimeMax = PlayerSkillConstant.SkillCoolTime[skillName];
        coolTime = 0;

        DashTargetMonster = new List<GameObject>();

        ghostDelayTime = 0;
        ghostDelayTimeMax = PlayerSkillConstant.ghostDelayTimeMax;
        GhostPrefab = Resources.Load(PrefabRouter.GhostPrefab) as GameObject;
    }

    public override void CheckInputKeyCode()
    {
        base.CheckInputKeyCode();

        Ghost();
    }

    protected override void UpdateKey()
    {
        keyCode = KeySetting.keys[Action.Mark_Dash];
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

        Vector2 playerBottom = playerTransform.position;
        Vector2 playerMiddle = playerBottom + new Vector2(0, PlayerConstant.playerHeight / 2);
        Vector2 playerTop = playerBottom + new Vector2(0, PlayerConstant.playerHeight);
        Vector2 endPoint = TargetMonster.transform.position;

        Vector2 direction = (endPoint - playerBottom).normalized;
        endPoint += new Vector2(PlayerSkillConstant.DashEndPointInterval * direction.x, PlayerSkillConstant.DashEndYOffset);

        float distance = Vector2.Distance(playerBottom, endPoint);
        CheckMonsterHitBox(playerBottom, direction, distance);
        CheckMonsterHitBox(playerMiddle, direction, distance);
        CheckMonsterHitBox(playerTop, direction, distance);

        // Dash시에 Player 머리와 발끝 경로가 보이는 Test용 코드 - 김민지, 20240901
        // Debug.DrawRay(playerBottom, dir, Color.red, distance);
        // Debug.DrawRay(playerMiddle, dir, Color.red, distance);
        // Debug.DrawRay(playerTop, dir, Color.red, distance);

        // 중복 제거
        DashTargetMonster = DashTargetMonster.Distinct().ToList();

        Player.Instance.Dashing(endPoint, true, true);

        foreach (GameObject obj in DashTargetMonster)
        {
            if (obj.CompareTag(TagConstant.Monster))
            {
                obj.GetComponent<Monster>().GetDamaged(damageMultiplier);
            }
        }

        DashTargetMonster.Clear();
    }

    protected override IEnumerator CountCoolTime()
    {
        coolTime = Player.Instance.GetSkillCoolTime(SkillName.Mark);

        while (coolTime > 0)
        {
            coolTime -= Time.deltaTime;
            yield return null;
        }

        coolTime = 0;
    }

    public override void ResetCoolTime()
    {
        if (countCoolTime != null) monoBehaviour.StopCoroutine(countCoolTime);
        coolTime = Player.Instance.GetSkillCoolTime(SkillName.Mark);
    }


    private void CheckMonsterHitBox(Vector2 origin, Vector2 Direction, float distance)
    {
        foreach (RaycastHit2D hit in Physics2D.RaycastAll(origin, Direction, distance))
        {
            DashTargetMonster.Add(hit.collider.gameObject);
        }
    }

    public void SetTarget(GameObject obj)
    {
        TargetMonster = obj;

        monoBehaviour.StartCoroutine(CountCoolTime());
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
