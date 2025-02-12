using System;
using System.Collections.Generic;

public static class TraitTierList
{
    private static Dictionary<SkillName, SkillTier> Info = new()
    {
        {SkillName.AppendMaxHP, SkillTier.Rare},
        {SkillName.DoubleJump, SkillTier.Rare},
        {SkillName.AppendAttack, SkillTier.Epic},
        {SkillName.KillRecoveryHP, SkillTier.Epic},
        {SkillName.Execution, SkillTier.Legendary},
        {SkillName.SkillReset, SkillTier.Legendary},
        {SkillName.Revive, SkillTier.Legendary},
    };

    public static SkillTier GetTier(SkillName skillName)
    {
        if (Info.ContainsKey(skillName)) return Info[skillName];
        return SkillTier.Common;
    }
}