using System;
using System.Collections.Generic;

public static class TraitPriceList
{
    public static Dictionary<SkillName, int> Info = new Dictionary<SkillName, int>
    {
        {SkillName.AppendMaxHP, 300},
        {SkillName.SkillReset, 400},
        {SkillName.DoubleJump, 200},
        {SkillName.Execution, 900},
        {SkillName.AppendAttack, 600},
        {SkillName.KillRecoveryHP, 500},
        {SkillName.Revive, 800},
    };

    public static void CheckTraitPriceListValidation()
    {
        SkillName[] skillNames = (SkillName[])Enum.GetValues(typeof(SkillName));
        int start = (int)SkillName.TraitSkill + 1;

        for (int i = start; i < (int)SkillName.End; i++)
        {
            if (!Info.ContainsKey(skillNames[i]))
            {
                throw new Exception($"No {skillNames[i]} is in TraitPriceList");
            }
        }
    }
}