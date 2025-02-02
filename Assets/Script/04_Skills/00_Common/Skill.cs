using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill
{
    protected GameObject unit;
    protected MonoBehaviour monoBehaviour;

    public SkillName skillName
    {
        get;
        protected set;
    }
    public string description;
    public SkillTier tier => TraitTierList.GetTier(skillName);

    public List<AdditionalEffect> additionalEffects = new();

    // CoolTime
    protected float coolTimeMax;
    protected float coolTime = 0;
    protected Coroutine countCoolTime;

    // Damage
    protected int damageMultiplier;

    protected GameObject EffectPrefab;

    public Skill(GameObject unit, string description = "")
    {
        this.unit = unit;
        this.description = description;
        monoBehaviour = unit.GetComponent<MonoBehaviour>();
        SetSkillName();

        if (PlayerSkillConstant.SkillCoolTime.ContainsKey(skillName)) coolTimeMax = PlayerSkillConstant.SkillCoolTime[skillName];
    }

    // You muse set the SkillName and Status!!
    protected abstract void SetSkillName();

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

    protected virtual void CreateEffectPrefab() { return; } // [Code Review - KMJ] Check the Necessity and "virtual" - SDH, 20240106
}