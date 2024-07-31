using UnityEngine;

public abstract class Skill
{
    public PLAYERSKILLNAME skillName;

    protected PLAYERSTATUS status;
    protected KeyCode key;

    protected string animTrigger;

    protected int atk;
    protected float cooltimeMax;
    protected float cooltime;

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

    protected virtual void UseSkill()
    {
        Player.Instance.SetPlayerStatus(status);
        Player.Instance.SetPlayerAnimTrigger(animTrigger);

        SkillMethod();
    }

    protected abstract void UpdateKey();

    protected abstract void SkillMethod();
}