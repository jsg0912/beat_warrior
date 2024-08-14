public class AppendMaxAttackCount : PassiveSkillBuff
{
    public override void GetPassiveSkill()
    {
        statKind = StatKind.ATTACKCOUNT;
        statBuff = 1;

        base.GetPassiveSkill();
    }
}
