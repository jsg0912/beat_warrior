using UnityEngine;

public class RecoveryHP : ActiveSkillPlayer
{
    public override void Initialize()
    {
        skillName = PlayerSkillName.RecoveryHP;

        coolTimeMax = PlayerSkillConstant.recoveryHPTimeMax;
        coolTime = coolTimeMax;
    }

    protected override void CountCoolTime()
    {
        if (Player.Instance.playerUnit.unitStat.GetIsFUllHP() == true)
        {
            coolTime = coolTimeMax;
            return;
        }

        if (coolTime > 0)
        {
            coolTime -= Time.deltaTime;
            return;
        }

        Player.Instance.ChangeCurrentHP(1);

        coolTime = coolTimeMax;
    }

    protected override void SkillMethod() { }

    protected override void UpdateKey() { }
}
