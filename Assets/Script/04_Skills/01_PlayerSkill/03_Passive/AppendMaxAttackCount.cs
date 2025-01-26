using UnityEngine;

public class AppendMaxAttackCount : PassiveSkillBuffPlayer
{
    public AppendMaxAttackCount(GameObject unit) : base(unit)
    {
        skillName = SkillName.AppendAttack;

        statKind = StatKind.AttackCount;
        statBuff = 1;
    }

}
