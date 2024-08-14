public class AppendMaxAttackCount : PassiveSkillBuffPlayer
{
    public override void GetSkill()
    {
        skillName = SkillName.AppendAttack;

        statKind = StatKind.AttackCount;
        statBuff = 1;

        base.GetSkill();
    }
}
