using UnityEngine;

public class SkillReset : PassiveSkill
{
    public SkillReset(GameObject unit) : base(unit) { }

    public override void GetSkill()
    {
        skillName = SkillName.SkillReset;
    }
}
