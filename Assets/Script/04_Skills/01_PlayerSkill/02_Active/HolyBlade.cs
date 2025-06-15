using System.Collections;
using UnityEngine;

// TODO: 충전형 스킬이라 기본 로직이 다른 스킬과 다르게 작동하는데, 이로 인해 버그가 자주 생길 수 있음, 모든 스킬에 대한 common 방식을 새로 생각하면 좋을 것 같음 - 신동환, 20250212
public class HolyBlade : ActiveSkillPlayer
{
    public HolyBlade(GameObject unit) : base(unit)
    {
        trigger = new() { PlayerConstant.attackAnimTrigger };
        damageMultiplier = PlayerSkillConstant.attackAtk;
        EffectPrefab = Resources.Load(PrefabRouter.PlayerAttackPrefab) as GameObject;
        additionalEffects.Add(new KnockBack(PlayerSkillConstant.attackKnockBackDistance));
    }

    protected override void SetSkillName() { skillName = SkillName.Attack; }

    protected override IEnumerator CountCoolTime()
    {
        coolTimer.Initialize();
        while (coolTimer.Tick())
        {
            yield return null;
        }

        Player.Instance.playerUnit.unitStat.ChangeCurrentStat(StatKind.AttackCount, 1);

        CheckCoolTime();
        AttackCountUI.Instance.UpdateUI();
    }

    protected override void TrySkill()
    {
        if (Player.Instance.playerUnit.unitStat.GetCurrentStat(StatKind.AttackCount) <= 0) return;

        UseSkill();
        CheckCoolTime();
    }

    private void CheckCoolTime()
    {
        if (GameManager.Instance.gameMode == GameMode.Infinite) return;

        if (coolTime > 0) return;

        if (!Player.Instance.playerUnit.GetIsFullStat(StatKind.AttackCount))
        {
            countCoolTime = unit.GetComponent<MonoBehaviour>().StartCoroutine(CountCoolTime());
            PlayerUIManager.Instance.SetCoolTimeUI(skillName);
        }
    }

    protected override void SetAttackCollider()
    {
        attackCollider = Player.Instance.colliderController.shortBladeCollider;
        base.SetAttackCollider();
    }

    protected override void UpdateKey()
    {
        keyCode = KeySetting.GetKey(PlayerAction.Attack);
    }

    protected override void SkillMethod()
    {
        if (GameManager.Instance.gameMode != GameMode.Infinite)
        {
            Player.Instance.playerUnit.unitStat.ChangeCurrentStat(StatKind.AttackCount, -1);
            AttackCountUI.Instance.UpdateUI();
        }
        SetAttackCollider(); // [Code Review - KMJ] Do not create new prefab every time, just use one obj and use activate/inactivate - SDH, 20250106
    }
}
