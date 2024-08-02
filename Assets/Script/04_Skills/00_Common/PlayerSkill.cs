using UnityEngine;

public abstract class PlayerSkill
{
    public PLAYERSKILLNAME skillName;

    protected PLAYERSTATUS status;
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

    protected void CreateAttackPrefab()
    {
        GameObject sword = GameObject.Instantiate(AttackPrefab);
        sword.transform.SetParent(Player.Instance.transform, false);
        sword.GetComponent<AttackCollider>().SetAtk(atk);
    }

    protected abstract void UpdateKey();

    protected abstract void SkillMethod();
}