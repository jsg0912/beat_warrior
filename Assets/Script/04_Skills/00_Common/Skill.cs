using System.Collections;
using UnityEngine;

public abstract class Skill
{
    protected GameObject unit;
    protected MonoBehaviour monoBehaviour;

    public SkillName skillName;
    public string description;

    // CoolTime
    protected float coolTimeMax;
    protected float coolTime;
    protected Coroutine countCoolTime;

    // Damage
    protected int damageMultiplier;

    protected GameObject EffectPrefab;

    public Skill(GameObject unit, string description = "")
    {
        this.unit = unit;
        this.description = description;
        monoBehaviour = unit.GetComponent<MonoBehaviour>();
    }

    public virtual void GetSkill() { return; }

    public virtual void RemoveSkill() { return; }

    public float GetCoolTime()
    {
        return coolTime;
    }

    public void StartCountCoolTime()
    {
        countCoolTime = monoBehaviour.StartCoroutine(CountCoolTime());
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

    public virtual void ResetCoolTime()
    {
        if (countCoolTime != null) monoBehaviour.StopCoroutine(countCoolTime);
        coolTime = 0;
    }

    protected virtual bool CreateEffectPrefab() { return false; }
}