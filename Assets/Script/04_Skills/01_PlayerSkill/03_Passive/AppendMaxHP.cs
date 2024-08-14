public class AppendMaxHP : PassiveSkillBuffPlayer
{
    public override void GetSkill()
    {
        skillName = SkillName.AppendMaxHP;

        statKind = StatKind.HP;
        statBuff = 1;

        base.GetSkill();
    }
}
