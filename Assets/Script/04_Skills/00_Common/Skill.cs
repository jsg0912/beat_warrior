using System.Collections;
using UnityEngine;

public abstract class Skill
{
    protected GameObject unit;
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

        GetSkill();
    }

    public virtual void GetSkill() { return; }

    public virtual void RemoveSkill() { return; }

    public float GetCoolTime()
    {
        return coolTime;
    }

    public void StartCountCoolTime()
    {
        countCoolTime = unit.GetComponent<MonoBehaviour>().StartCoroutine(CountCoolTime());
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
        if (countCoolTime != null) unit.GetComponent<MonoBehaviour>().StopCoroutine(countCoolTime);
        coolTime = 0;
    }

    protected virtual void CreateAttackPrefab() { return; }
}