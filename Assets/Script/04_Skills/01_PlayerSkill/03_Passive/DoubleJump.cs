public class DoubleJump : PassiveSkillBuffPlayer
{
    public override void GetSkill()
    {
        skillName = SkillName.DoubleJump;

        statKind = StatKind.JumpCount;
        statBuff = 1;

        base.GetSkill();
    }
}
