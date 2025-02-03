using System.Collections.Generic;

public class OutGameUIConstant
{
    public static Dictionary<SkillTier, string> TraitTireViewText = new Dictionary<SkillTier, string>
    {
        { SkillTier.Common, "-" },
        { SkillTier.Rare, "I" },
        { SkillTier.Epic, "II" },
        { SkillTier.Legendary, "III" }
    };
}