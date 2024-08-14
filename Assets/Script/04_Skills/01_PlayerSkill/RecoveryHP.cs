using UnityEngine;

public class RecoveryHP : PlayerSkill
{
    public override void Initialize()
    {
        skillName = PLAYERSKILLNAME.RECOVERYHP;

        cooltimeMax = PlayerSkillConstant.recoveryHPTimeMax;
        cooltime = cooltimeMax;
    }

    protected override void CountCooltime()
    {
        if (Player.Instance.GetHP() == Player.Instance.GetFinalStat(StatKind.HP))
        {
            cooltime = cooltimeMax;
            return;
        }

        if (cooltime > 0)
        {
            cooltime -= Time.deltaTime;
            return;
        }

        Player.Instance.ChangeCurrentHP(1);

        cooltime = cooltimeMax;
    }

    protected override void SkillMethod() { }

    protected override void UpdateKey() { }
}
