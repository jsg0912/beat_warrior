using UnityEngine;

public class DoubleJump : PassiveSkillBuffPlayer
{
    public DoubleJump(GameObject unit) : base(unit)
    {
        statKind = StatKind.JumpCount;
        statBuff = 1;
    }

    protected override void SetSkillName() { skillName = SkillName.DoubleJump; }
}
