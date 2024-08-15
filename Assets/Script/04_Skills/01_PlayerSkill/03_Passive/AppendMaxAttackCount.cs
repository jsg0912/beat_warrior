using UnityEngine;

public class AppendMaxAttackCount : PassiveSkillBuffPlayer
{
    public AppendMaxAttackCount(GameObject unit) : base(unit) { }

    public override void GetSkill()
    {
        skillName = SkillName.AppendAttack;

        statKind = StatKind.AttackCount;
        statBuff = 1;

        base.GetSkill();
    }
}
