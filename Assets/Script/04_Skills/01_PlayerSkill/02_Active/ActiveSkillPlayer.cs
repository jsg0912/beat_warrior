using UnityEngine;

public abstract class ActiveSkillPlayer : ActiveSkill
{
    protected KeyCode keyCode;
    protected PlayerStatus status;

    protected ActiveSkillPlayer(GameObject unit) : base(unit) { }

    protected override void UseSkill()
    {
        Player.Instance.SetPlayerStatus(status);

        SkillMethod();

        if (Player.Instance.useSKillFuncList != null) Player.Instance.useSKillFuncList(this);
    }

    public virtual void CheckInputKeyCode()
    {
        UpdateKey();

        if (Input.GetKeyDown(keyCode))
        {
            if (Player.Instance.IsUsingSkill() == true) return;

            TrySkill();
        }
    }

    protected override void CreateAttackPrefab()
    {
        GameObject attackPrefab = GameObject.Instantiate(EffectPrefab);
        attackPrefab.transform.SetParent(Player.Instance.transform, false);
        attackPrefab.GetComponent<AttackCollider>().SetAtk(damageMultiplier);
    }

    protected abstract void UpdateKey();
}