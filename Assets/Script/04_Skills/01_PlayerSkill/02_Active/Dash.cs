using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Dash : ActiveSkillPlayer
{
    private Timer dashAvailableTimer;
    private List<GameObject> attackedMonsterByDash = new();
    public GameObject targetMonster { get; private set; }

    public Dash(GameObject unit) : base(unit)
    {
        trigger = new() { PlayerConstant.dashAnimTrigger };
        damageMultiplier = PlayerSkillConstant.dashAtk;
        dashAvailableTimer = coolTimer;
    }

    protected override void SetSkillName() { skillName = SkillName.Dash; }

    public override void CheckInputKeyCode(bool forced = false)
    {
        base.CheckInputKeyCode();
    }

    protected override void UpdateKey()
    {
        keyCode = KeySetting.GetKey(PlayerAction.Mark_Dash);
    }

    protected override void TrySkill()
    {
        if (targetMonster == null) return;

        if (dashAvailableTimer.remainTime <= 0) return;

        UseSkill();
    }

    protected override void SkillMethod()
    {
        dashAvailableTimer.SetRemainTimeZero();
        PlayerUIManager.Instance.SwapMarkAndDash(true);

        SoundManager.Instance.SFXPlay("Equip", SoundList.Instance.playerDash);

        Vector2 playerBottom = Player.Instance.GetBottomPos();
        Vector2 endPoint = targetMonster.GetComponent<Monster>().GetBottomPos();

        Vector2 direction = (endPoint - playerBottom).normalized;
        if (endPoint.x < playerBottom.x)
        {
            endPoint += new Vector2(-PlayerSkillConstant.DashEndPointInterval, 0);
        }
        else
        {
            endPoint += new Vector2(PlayerSkillConstant.DashEndPointInterval, 0);
        }

        Player.Instance.Dashing(endPoint, true, true);
        StartDealTrigger(playerBottom, endPoint, direction);
    }

    protected IEnumerator CountDashAvailableTimer()
    {
        dashAvailableTimer.Initialize(Player.Instance.GetSkillCoolTime(SkillName.Mark));
        while (dashAvailableTimer.Tick())
        {
            yield return null;
        }
    }

    protected override void SetAttackCollider()
    {
        attackCollider = Player.Instance.colliderController.BodyAttackCollider;
        base.SetAttackCollider();
    }

    // Dash의 속도가 매우 빨라서 일반적인 Collider 판정으로 딜을 넣기 불가능하기 때문에 따로 Ray를 쏴서 판정을 내려야 함
    private void StartDealTrigger(Vector2 playerBottom, Vector2 endPoint, Vector2 direction)
    {
        Vector2 playerMiddle = Player.Instance.GetMiddlePos();
        Vector2 playerTop = playerBottom + new Vector2(0, PlayerConstant.playerHeight);

        float distance = Vector2.Distance(playerBottom, endPoint);

        CheckMonsterHitBox(playerBottom, direction, distance);
        CheckMonsterHitBox(playerMiddle, direction, distance);
        CheckMonsterHitBox(playerTop, direction, distance);
        attackedMonsterByDash = attackedMonsterByDash.Distinct().ToList();

        // Dash시에 Player 머리와 발끝 경로가 보이는 Test용 코드 - 김민지, 20240901
        // Debug.DrawRay(playerBottom, direction * distance, Color.red, distance);
        // Debug.DrawRay(playerMiddle, direction * distance, Color.red, distance);
        // Debug.DrawRay(playerTop, direction * distance, Color.red, distance);

        foreach (GameObject obj in attackedMonsterByDash)
        {
            if (obj.layer == LayerMask.NameToLayer(LayerConstant.Monster))
            {
                obj.GetComponent<MonsterBodyCollider>().monster.AttackedByPlayer(damageMultiplier * Player.Instance.GetFinalStat(StatKind.ATK));
            }
        }

        attackedMonsterByDash.Clear();
    }

    private void CheckMonsterHitBox(Vector2 origin, Vector2 Direction, float distance)
    {
        foreach (RaycastHit2D hit in Physics2D.RaycastAll(origin, Direction, distance))
        {
            attackedMonsterByDash.Add(hit.collider.gameObject);
        }
    }

    public void SetTarget(GameObject obj)
    {
        targetMonster = obj;

        countCoolTime = monoBehaviour.StartCoroutine(CountDashAvailableTimer());
    }
}
