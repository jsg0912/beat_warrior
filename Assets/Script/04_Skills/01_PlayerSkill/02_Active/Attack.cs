using System.Collections;
using UnityEngine;

public class Attack : ActiveSkillPlayer
{
    private int attackCount;
    private bool isCharging;

    public Attack(GameObject unit) : base(unit) { }

    public override void GetSkill()
    {
        skillName = SkillName.Attack;
        status = PlayerStatus.Attack;

        damageMultiplier = PlayerSkillConstant.attackAtk;
        attackCount = PlayerSkillConstant.attackCountMax;

        isCharging = false;

        coolTimeMax = PlayerSkillConstant.attackChargeTimeMax;
        coolTime = 0;

        EffectPrefab = Resources.Load(PlayerSkillConstant.attackPrefab) as GameObject;
    }

    public int GetAttackCount()
    {
        return attackCount;
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

        attackCount++;

        CheckCoolTime();
    }

    protected override void TrySkill()
    {
        if (attackCount <= 0) return;

        UseSkill();
        CheckCoolTime();
    }

    public void CheckCoolTime()
    {
        if (isCharging == true) return;

        if (attackCount < Player.Instance.GetFinalStat(StatKind.AttackCount))
            unit.GetComponent<MonoBehaviour>().StartCoroutine(CountCoolTime());
    }

    protected override void UpdateKey()
    {
        keyCode = KeySetting.keys[Action.Attack];
    }

    protected override void SkillMethod()
    {
        attackCount--;

        CreateAttackPrefab();
    }
}
