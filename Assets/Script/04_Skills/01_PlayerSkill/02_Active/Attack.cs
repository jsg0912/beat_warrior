using System.Collections;
using UnityEngine;

public class Attack : ActiveSkillPlayer
{
    private bool isCharging;
    private KnockBack knockBack;

    public Attack(GameObject unit) : base(unit)
    {
        skillName = SkillName.Attack;
        status = PlayerStatus.Attack;

        damageMultiplier = PlayerSkillConstant.attackAtk;

        isCharging = false;

        coolTimeMax = PlayerSkillConstant.SkillCoolTime[skillName];
        coolTime = 0;

        EffectPrefab = Resources.Load(PrefabRouter.PlayerAttackPrefab) as GameObject;
        knockBack = new KnockBack(PlayerSkillConstant.attackKnockBackDistance);
    }

    protected override IEnumerator CountCoolTime()
    {
        coolTime = coolTimeMax;
        isCharging = true;

        while (coolTime > 0)
        {
            coolTime -= Time.deltaTime;
            yield return null;
        }

        coolTime = 0;
        isCharging = false;
        Util.SetActive(attackCollider.gameObject, false);

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
        if (isCharging == true) return;

        if (Player.Instance.playerUnit.GetIsFullStat(StatKind.AttackCount) == false)
            unit.GetComponent<MonoBehaviour>().StartCoroutine(CountCoolTime());
    }

    protected override bool CreateEffectPrefab()
    {
        bool isMade = base.CreateEffectPrefab();
        if (isMade) attackCollider.SetAdditionalEffect(knockBack);

        return isMade;
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
