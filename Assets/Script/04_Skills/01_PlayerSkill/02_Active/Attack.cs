using UnityEngine;

public class Attack : ActiveSkillPlayer
{
    private int attackPoint;
    private int attackPointMax;

    public override void Initialize()
    {
        skillName = PLAYERSKILLNAME.ATTACK;
        status = PLAYERSTATUS.ATTACK;

        damageMultiplier = PlayerSkillConstant.attackAtk;
        attackPoint = PlayerSkillConstant.attackPointMax;
        attackPointMax = PlayerSkillConstant.attackPointMax;

        coolTimeMax = PlayerSkillConstant.attackChargeTimeMax;
        coolTime = 0;

        EffectPrefab = Resources.Load(PlayerSkillConstant.attackPrefab) as GameObject;
    }

    public int GetAttackPoint()
    {
        return attackPoint;
    }

    protected override void CountCoolTime()
    {
        if (coolTime > 0)
        {
            coolTime -= Time.deltaTime;
            return;
        }

        if (attackPoint == attackPointMax)
        {
            coolTime = 0;
            return;
        }

        attackPoint++;

        coolTime = coolTimeMax;
    }

    protected override void TrySkill()
    {
        if (attackPoint <= 0) return;

        if (attackPoint == attackPointMax) coolTime = coolTimeMax;

        UseSkill();
    }

    protected override void UpdateKey()
    {
        keyCode = KeySetting.keys[ACTION.ATTACK];
    }

    protected override void SkillMethod()
    {
        attackPoint--;

        CreateAttackPrefab();
    }
}
