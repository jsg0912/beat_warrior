using System.Collections;
using UnityEngine;

public class Skill
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

    public virtual void UpdateSkill() { }

    protected virtual void TrySkill()
    {
        if (coolTime > 0) return;

        UseSkill();

        unit.GetComponent<MonoBehaviour>().StartCoroutine(CountCoolTime());
    }

    protected virtual void UseSkill() { return; }

    protected virtual void SkillMethod() { return; }

    protected virtual void CreateAttackPrefab() { return; }

    public virtual void GetSkill() { return; }

    public virtual void RemoveSkill() { return; }
}