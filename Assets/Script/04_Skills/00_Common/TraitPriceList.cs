using System;
using System.Collections.Generic;

public static class TraitPriceList
{
    public static Dictionary<SkillName, int> Info = new Dictionary<SkillName, int>
    {
        // {SkillName.AppendMaxHP, },
        // {SkillName.SkillReset, },
        // {SkillName.DoubleJump, },
        // {SkillName.AddDmg, },
        // {SkillName.Execution, },
        // {SkillName.Resistance, },
        // {SkillName.AppendAttack, },
        // {SkillName.CounterAttack, },
        // {SkillName.KillRecoveryHP, },
    };

    public static void CheckTraitPriceListValidation()
    {
        SkillName[] skillNames = (SkillName[])Enum.GetValues(typeof(SkillName));
        int start = (int)SkillName.TraitSkill + 1;

        for (int i = start; i < (int)SkillName.End; i++)
        {
            if (Info.ContainsKey(skillNames[i]) == false)
            {

            }
        }
    }
}