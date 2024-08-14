using UnityEngine;

public class Skill
{
    // CoolTime
    protected float coolTimeMax;
    protected float coolTime;

    // Damage
    protected int damageMultiplier;

    protected GameObject EffectPrefab;

    public virtual void Initialize() { return; }

    public float GetCoolTime()
    {
        return coolTime;
    }

    protected virtual void CountCoolTime()
    {
        if (coolTime <= 0) return;
        coolTime -= Time.deltaTime;
    }

    public void ResetCoolTime()
    {
        coolTime = 0;
    }

    public virtual void UpdateSkill()
    {
        CountCoolTime();
    }

    protected virtual void TrySkill()
    {
        if (coolTime > 0) return;

        UseSkill();
        coolTime = coolTimeMax;
    }

    protected virtual void UseSkill() { return; }

    protected virtual void SkillMethod() { return; }

    protected virtual void CreateAttackPrefab() { return; }
}