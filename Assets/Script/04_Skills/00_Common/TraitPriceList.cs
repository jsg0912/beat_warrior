using System;
using System.Collections.Generic;

public static class TraitPriceList
{
    public static Dictionary<SkillName, int> Info = new Dictionary<SkillName, int>
    {
        {SkillName.AppendMaxHP, 100},
        {SkillName.SkillReset, 100},
        {SkillName.DoubleJump, 100},
        {SkillName.Execution, 100},
        {SkillName.AppendAttack, 150},
        {SkillName.KillRecoveryHP, 150},
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