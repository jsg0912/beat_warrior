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
        DebugConsole.Log("TryReset");
        if (ResetTargetSkills.Contains(skill.skillName) == false)
        {
            return;
        }
        DebugConsole.Log("Skill in Target");

        float probability = Random.Range(0.0f, 1.0f);

        DebugConsole.Log(probability);
        if (probability < PlayerSkillConstant.SkillResetProbability)
        {
            skill.ResetCoolTime();
            DebugConsole.Log("Reset Success");
        }
    }
}
