public class AppendMaxHP : PassiveSkillBuff
{
    public override void GetPassiveSkill()
    {
        statKind = StatKind.HP;
        statBuff = 1;

        base.GetPassiveSkill();
    }
}
