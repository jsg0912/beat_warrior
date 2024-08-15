using UnityEngine;

public class DoubleJump : PassiveSkillBuffPlayer
{
    public DoubleJump(GameObject unit) : base(unit) { }

    public override void GetSkill()
    {
        skillName = SkillName.DoubleJump;

        statKind = StatKind.JumpCount;
        statBuff = 1;

        base.GetSkill();
    }
}
