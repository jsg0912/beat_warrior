using UnityEngine;

public abstract class Skill
{
    protected KeyCode key;

    protected string animTrigger;

    protected int atk;
    protected float cooltimeMax;
    protected float cooltime;

    protected GameObject AttackPrefab;

    public abstract void Initialize();

    public float GetCooltime()
    {
        return cooltime;
    }

    protected virtual void CountCooltime()
    {
        if (cooltime <= 0)
        {
            cooltime = 0;
            return;
        }

        cooltime -= Time.deltaTime;
    }

    public void ResetCoolTime()
    {
        cooltime = 0;
    }

    public virtual void CheckSkill()
    {
        if (!Player.Instance.IsSkillUseable()) return;

        CountCooltime();
        UpdateKey();

        if (Input.GetKeyDown(key))
        {
            PlaySkill();
        }
    }

    protected virtual void PlaySkill()
    {
        if (cooltime > 0) return;

        UseSkill();

        cooltime = cooltimeMax;
    }

    protected abstract void UseSkill();

    protected abstract void CreateAttackPrefab();

    protected abstract void UpdateKey();

    protected abstract void SkillMethod();
}