public class AppendMaxHP : PassiveSkillBuffPlayer
{
    public override void GetSkill()
    {
        skillName = SkillName.AppendHP;

        statKind = StatKind.HP;
        statBuff = 1;

        base.GetSkill();
    }
}
