using UnityEditor.Experimental.GraphView;
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
        if (Player.Instance.GetPlayerStatus() != PLAYERSTATUS.DEAD) PlaySkill();
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

    protected virtual void PlaySkill()
    {
        UpdateKey();

        if (Input.GetKeyDown(key))
        {
            if (cooltime > 0) return;

            SkillMethod();

            Player.Instance.SetPlayerStatus(status);
            Player.Instance.SetAnimTrigger(animTrigger);

            cooltime = cooltimeMax;
        }
    }

    protected abstract void UpdateKey();

    protected abstract void SkillMethod();
}