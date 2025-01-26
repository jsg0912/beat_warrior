using UnityEngine;

public abstract class PassiveSkill : Skill
{
    protected PassiveSkill(GameObject unit, SkillTier skillTier) : base(unit, skillTier) { }
}