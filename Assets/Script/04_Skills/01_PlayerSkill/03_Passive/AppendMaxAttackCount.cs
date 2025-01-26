using UnityEngine;

public class AppendMaxAttackCount : PassiveSkillBuffPlayer
{
    public AppendMaxAttackCount(GameObject unit) : base(unit, SkillTier.Epic)
    {
        skillName = SkillName.AppendAttack;

        statKind = StatKind.AttackCount;
        statBuff = 1;
    }

}
