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

        Vector2 playerBottom = playerTransform.position;
        Vector2 playerMiddle = playerBottom + new Vector2(0, PlayerConstant.playerHeight / 2);
        Vector2 playerTop = playerBottom + new Vector2(0, PlayerConstant.playerHeight);
        Vector2 endPoint = TargetMonster.transform.position;

        int offset = endPoint.x > playerBottom.x ? 1 : -1;
        endPoint += new Vector2(PlayerSkillConstant.DashEndPointInterval * offset, 0);

        Vector2 dir = endPoint - playerBottom;
        float distance = Vector2.Distance(playerBottom, endPoint);
        CheckMonsterHitBox(playerBottom, dir, distance);
        CheckMonsterHitBox(playerMiddle, dir, distance);
        CheckMonsterHitBox(playerTop, dir, distance);

        // Dash시에 Player 머리와 발끝 경로가 보이는 Test용 코드 - 김민지, 20240901
        // Debug.DrawRay(playerBottom, dir, Color.red, distance);
        // Debug.DrawRay(playerMiddle, dir, Color.red, distance);
        // Debug.DrawRay(playerTop, dir, Color.red, distance);

        // 중복 제거
        DashTargetMonster = DashTargetMonster.Distinct().ToList();

        Player.Instance.Dashing(endPoint, true, true);

        foreach (GameObject obj in DashTargetMonster)
        {
            if (obj.CompareTag("Monster"))
            {
                obj.GetComponent<Monster>().GetDamaged(damageMultiplier);
            }
        }

        DashTargetMonster.Clear();
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
