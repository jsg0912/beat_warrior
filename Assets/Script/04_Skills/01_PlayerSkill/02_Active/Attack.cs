using UnityEngine;

public class Attack : ActiveSkillPlayer
{
    private int attackCount;
    private int attackCountMax;

    public override void Initialize()
    {
        skillName = PlayerSkillName.Attack;
        status = PlayerStatus.Attack;

        damageMultiplier = PlayerSkillConstant.attackAtk;
        attackCount = PlayerSkillConstant.attackCountMax;
        attackCountMax = PlayerSkillConstant.attackCountMax;

        coolTimeMax = PlayerSkillConstant.attackChargeTimeMax;
        coolTime = 0;

        EffectPrefab = Resources.Load(PlayerSkillConstant.attackPrefab) as GameObject;
    }

    public int GetAttackCount()
    {
        return attackCount;
    }

    protected override void CountCoolTime()
    {
        if (coolTime > 0)
        {
            coolTime -= Time.deltaTime;
            return;
        }

        if (attackCount == attackCountMax)
        {
            coolTime = 0;
            return;
        }

        attackCount++;

        coolTime = coolTimeMax;
    }

    protected override void TrySkill()
    {
        if (attackCount <= 0) return;

        if (attackCount == attackCountMax) coolTime = coolTimeMax;

        UseSkill();
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
