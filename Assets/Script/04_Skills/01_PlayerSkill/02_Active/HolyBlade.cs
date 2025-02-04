using System.Collections;
using UnityEngine;

public class HolyBlade : ActiveSkillPlayer
{
    private bool isCharging => coolTime > 0;

    public HolyBlade(GameObject unit) : base(unit)
    {
        trigger = new() { PlayerConstant.attackLAnimTrigger, PlayerConstant.attackRAnimTrigger };
        damageMultiplier = PlayerSkillConstant.attackAtk;
        EffectPrefab = Resources.Load(PrefabRouter.PlayerAttackPrefab) as GameObject;
        additionalEffects.Add(new KnockBack(PlayerSkillConstant.attackKnockBackDistance));
    }

    protected override void SetSkillName() { skillName = SkillName.Attack; }

    protected override IEnumerator CountCoolTime()
    {
        coolTime = coolTimeMax;

        while (isCharging)
        {
            coolTime -= Time.deltaTime;
            yield return null;
        }

        coolTime = 0;

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

    public void CheckCoolTime()
    {
        if (isCharging) return;

        if (!Player.Instance.playerUnit.GetIsFullStat(StatKind.AttackCount))
            unit.GetComponent<MonoBehaviour>().StartCoroutine(CountCoolTime());
    }

    protected override void CreateEffectPrefab()
    {
        attackCollider = Player.Instance.colliderController.shortBladeCollider;
        base.CreateEffectPrefab();
    }

    protected override void UpdateKey()
    {
        keyCode = KeySetting.keys[PlayerAction.Attack];
    }

    protected override void SkillMethod()
    {
        Player.Instance.playerUnit.unitStat.ChangeCurrentStat(StatKind.AttackCount, -1);
        AttackCountUI.Instance.UpdateUI();
        CreateEffectPrefab(); // [Code Review - KMJ] Do not create new prefab every time, just use one obj and use activate/inactivate - SDH, 20250106
    }
}
