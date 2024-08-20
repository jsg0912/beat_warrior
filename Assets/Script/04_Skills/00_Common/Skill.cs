using System.Collections;
using UnityEngine;

public abstract class Skill
{
    protected GameObject unit;
    public SkillName skillName;

    // CoolTime
    protected float coolTimeMax;
    protected float coolTime;

    // Damage
    protected int damageMultiplier;

    protected GameObject EffectPrefab;

    public Skill(GameObject unit)
    {
        this.unit = unit;

        GetSkill();
    }

    public virtual void GetSkill() { return; }

    public virtual void RemoveSkill() { return; }

    public float GetCoolTime()
    {
        return coolTime;
    }

    protected virtual IEnumerator CountCoolTime()
    {
        coolTime = coolTimeMax;

        while (coolTime > 0)
        {
            coolTime -= Time.deltaTime;
            yield return null;
        }

        coolTime = 0;
    }

    public void ResetCoolTime()
    {
        coolTime = 0;
    }

    protected virtual void TrySkill()
    {
        if (coolTime > 0) return;

        UseSkill();

        unit.GetComponent<MonoBehaviour>().StartCoroutine(CountCoolTime());
    }

    protected abstract void UseSkill();

    protected abstract void SkillMethod();

    protected virtual void CreateAttackPrefab() { return; }
}