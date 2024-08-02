using UnityEngine;

public class Attack : PlayerSkill
{
    private int attackPoint;

    public override void Initialize()
    {
        skillName = PLAYERSKILLNAME.ATTACK;
        status = PLAYERSTATUS.ATTACK;
        animTrigger = PlayerSkillConstant.attackAnimTrigger;

        atk = PlayerSkillConstant.attackAtk;
        attackPoint = PlayerSkillConstant.attackPointMax;

        cooltimeMax = PlayerSkillConstant.attackChargeTimeMax;
        cooltime = 0;

        AttackPrefab = Resources.Load(PlayerSkillConstant.attackPrefab) as GameObject;
    }

    public int GetAttackPoint()
    {
        return attackPoint;
    }

    protected override void CountCooltime()
    {
        if (cooltime > 0)
        {
            cooltime -= Time.deltaTime;
            return;
        }

        if (attackPoint == PlayerSkillConstant.attackPointMax)
        {
            cooltime = 0;
            return;
        }

        attackPoint++;

        cooltime = cooltimeMax;
    }

    protected override void PlaySkill()
    {
        if (attackPoint <= 0) return;

        if (attackPoint == PlayerSkillConstant.attackPointMax) cooltime = cooltimeMax;

        UseSkill();
    }

    protected override void UpdateKey()
    {
        key = KeySetting.keys[ACTION.ATTACK];
    }

    protected override void SkillMethod()
    {
        attackPoint--;

        CreateAttackPrefab();
    }
}
