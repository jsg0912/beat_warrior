using UnityEngine;

public class RecoveryHP : PlayerSkill
{
    private int hpMax;

    public override void Initialize()
    {
        skillName = PLAYERSKILLNAME.RECOVERYHP;

        hpMax = PlayerConstant.hpMax;

        cooltimeMax = PlayerSkillConstant.recoveryHPTimeMax;
        cooltime = cooltimeMax;
    }

    protected override void CountCooltime()
    {
        if (Player.Instance.GetHP() == hpMax)
        {
            cooltime = cooltimeMax;
            return;
        }

        if (cooltime > 0)
        {
            cooltime -= Time.deltaTime;
            return;
        }

        Player.Instance.SetHP(Player.Instance.GetHP() + 1);

        cooltime = cooltimeMax;
    }

    protected override void SkillMethod() { }

    protected override void UpdateKey() { }
}
