using UnityEngine;

public abstract class ActiveSkill : Skill
{
    protected ActiveSkill(GameObject unit, SkillTier skillTier) : base(unit, skillTier) { }

    protected virtual void TrySkill()
    {
        if (coolTime > 0) return;

        StartCountCoolTime();

        UseSkill();
    }

    protected abstract void UseSkill();

    protected abstract void SkillMethod();
}