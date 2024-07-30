using UnityEngine;

public class Attack : Skill
{
    private int attackPoint;

    void Start()
    {
        status = PLAYERSTATUS.ATTACK;
        animTrigger = PlayerSkillConstant.attackAnimTrigger;

        attackPoint = PlayerSkillConstant.attackPointMax;

        cooltimeMax = PlayerSkillConstant.attackChargeTimeMax;
        cooltime = 0;
    }

    protected override void CountCooltime()
    {
        if (cooltime > 0)
        {
            cooltime -= Time.deltaTime;
            return;
        }

        attackPoint++;

        if (attackPoint == PlayerSkillConstant.attackPointMax)
        {
            cooltime = 0;
            return;
        }

        cooltime = cooltimeMax;
    }

    protected override void PlaySkill()
    {
        UpdateKey();

        if (Input.GetKeyDown(key))
        {
            if (attackPoint <= 0) return;

            if (attackPoint == PlayerSkillConstant.attackPointMax) cooltime = cooltimeMax;

            Player.Instance.SetPlayerStatus(status);
            Player.Instance.SetAnimTrigger(animTrigger);

            SkillMethod();
        }
    }

    protected override void UpdateKey()
    {
        key = KeySetting.keys[ACTION.ATTACK];
    }

    protected override void SkillMethod()
    {
        attackPoint--;
    }
}
