using System.Collections.Generic;
using UnityEngine;

public class SkillReset : PassiveSkill
{
    public SkillReset(GameObject unit) : base(unit)
    {
        skillName = SkillName.SkillReset;
    }

    private List<SkillName> ResetTargetSkills = new List<SkillName>
    {
        SkillName.Dash,
        SkillName.Skill1,
        SkillName.Skill2,
    };

    public override void GetSkill()
    {
        Player.Instance.useSKillFuncList += TryReset;
    }

    public override void RemoveSkill()
    {
        Player.Instance.useSKillFuncList -= TryReset;
    }

    public void TryReset(Skill skill)
    {
        if (!ResetTargetSkills.Contains(skill.skillName))
        {
            return;
        }

        float probability = Random.Range(0.0f, 1.0f);
        if (probability < PlayerSkillConstant.SkillResetProbability)
        {
            skill.ResetCoolTime();
        }
    }
}
