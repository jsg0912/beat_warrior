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
    protected Timer coolTimer;
    public float coolTime => countCoolTime == null ? 0 : coolTimer.remainTime;
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

        if (PlayerSkillConstant.SkillCoolTime.ContainsKey(skillName)) coolTimer = new Timer(PlayerSkillConstant.SkillCoolTime[skillName]);
    }

    // You muse set the SkillName and Status!!
    protected abstract void SetSkillName();

    public virtual void GetSkill() { return; }

    public virtual void RemoveSkill() { return; }

    public void StartCountCoolTime()
    {
        countCoolTime = monoBehaviour.StartCoroutine(CountCoolTime());
    }

    protected virtual IEnumerator CountCoolTime()
    {
        coolTimer.Initialize();
        while (coolTimer.Tick())
        {
            yield return null;
        }
    }

    public virtual void ResetCoolTime()
    {
        if (countCoolTime != null)
        {
            monoBehaviour.StopCoroutine(countCoolTime);
            coolTimer.SetRemainTimeZero();
            PlayerUIManager.Instance.ResetCoolTImeUI(skillName);
        }
    }

    protected virtual void SetAttackCollider() { return; } // [Code Review - KMJ] Check the Necessity and "virtual" - SDH, 20240106
}