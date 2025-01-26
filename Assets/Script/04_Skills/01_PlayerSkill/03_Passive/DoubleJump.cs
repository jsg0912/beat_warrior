using UnityEngine;

public class DoubleJump : PassiveSkillBuffPlayer
{
    public DoubleJump(GameObject unit) : base(unit)
    {
        skillName = SkillName.DoubleJump;

        statKind = StatKind.JumpCount;
        statBuff = 1;
    }
}
