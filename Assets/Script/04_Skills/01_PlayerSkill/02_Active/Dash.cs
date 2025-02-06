using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Dash : ActiveSkillPlayer
{
    private List<GameObject> DashTargetMonster = new();
    private GameObject TargetMonster;

    private float ghostDelayTime = 0;
    private float ghostDelayTimeMax;
    private GameObject GhostPrefab;

    public Dash(GameObject unit) : base(unit)
    {
        trigger = new() { PlayerConstant.dashAnimTrigger };

        damageMultiplier = PlayerSkillConstant.dashAtk;

        ghostDelayTimeMax = PlayerSkillConstant.ghostDelayTimeMax;
        GhostPrefab = Resources.Load(PrefabRouter.GhostPrefab) as GameObject;
    }

    protected override void SetSkillName() { skillName = SkillName.Dash; }

    public override void CheckInputKeyCode()
    {
        base.CheckInputKeyCode();

        Ghost();
    }

    protected override void UpdateKey()
    {
        keyCode = KeySetting.keys[PlayerAction.Mark_Dash];
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
        PlayerUIManager.Instance.SwapMarkAndDash(true);

        Vector2 playerBottom = Player.Instance.GetBottomPos();
        Vector2 endPoint = TargetMonster.GetComponent<Monster>().GetBottomPos();

        Vector2 direction = (endPoint - playerBottom).normalized;
        endPoint += new Vector2(PlayerSkillConstant.DashEndPointInterval * direction.x, PlayerSkillConstant.DashEndYOffset);

        Player.Instance.Dashing(endPoint, true, true);
        STartDealTrigger(playerBottom, endPoint, direction);
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

    protected override void CreateEffectPrefab()
    {
        attackCollider = Player.Instance.colliderController.BodyAttackCollider;
        base.CreateEffectPrefab();
    }

    public override void ResetCoolTime()
    {
        if (countCoolTime != null) monoBehaviour.StopCoroutine(countCoolTime);
        coolTime = Player.Instance.GetSkillCoolTime(SkillName.Mark);
    }

    // Dash의 속도가 매우 빨라서 일반적인 Collider 판정으로 딜을 넣기 불가능하기 때문에 따로 Ray를 쏴서 판정을 내려야 함
    private void STartDealTrigger(Vector2 playerBottom, Vector2 endPoint, Vector2 direction)
    {
        Vector2 playerMiddle = Player.Instance.GetMiddlePos();
        Vector2 playerTop = playerBottom + new Vector2(0, PlayerConstant.playerHeight);

        float distance = Vector2.Distance(playerBottom, endPoint);

        CheckMonsterHitBox(playerBottom, direction, distance);
        CheckMonsterHitBox(playerMiddle, direction, distance);
        CheckMonsterHitBox(playerTop, direction, distance);
        DashTargetMonster = DashTargetMonster.Distinct().ToList();

        // Dash시에 Player 머리와 발끝 경로가 보이는 Test용 코드 - 김민지, 20240901
        // Debug.DrawRay(playerBottom, direction * distance, Color.red, distance);
        // Debug.DrawRay(playerMiddle, direction * distance, Color.red, distance);
        // Debug.DrawRay(playerTop, direction * distance, Color.red, distance);

        foreach (GameObject obj in DashTargetMonster)
        {
            if (obj.layer == LayerMask.NameToLayer(LayerConstant.Monster))
            {
                obj.GetComponent<MonsterBodyCollider>().monster.AttackedByPlayer(damageMultiplier * Player.Instance.GetFinalStat(StatKind.ATK));
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

        //if (Player.Instance.GetPlayerStatus() != PlayerStatus.Dash) return;

        GameObject ghost = GameObject.Instantiate(GhostPrefab, Player.Instance.transform.position, Quaternion.identity);
        ghostDelayTime = ghostDelayTimeMax;
        GameObject.Destroy(ghost, 1.0f);
    }
}
