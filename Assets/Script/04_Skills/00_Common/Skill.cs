using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    protected PLAYERSTATUS status;
    protected KeyCode key;

    protected string animTrigger;

    protected float cooltimeMax;
    protected float cooltime;

    protected void Update()
    {
        CountCooltime();
        if (Player.Instance.GetPlayerStatus() != PLAYERSTATUS.DEAD) CheckSkill();
    }

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

    protected virtual void CheckSkill()
    {
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