using UnityEngine;

public abstract class ActiveSkillPlayer : ActiveSkill
{
    protected KeyCode keyCode;
    protected PlayerStatus status;
    protected AttackCollider attackCollider; // TODO: if there are different type of active skill without "attack", then we have to divide this function's contents - SDH, 20250106

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

    // TODO: if there are different type of active skill without "attack", then we have to divide this function's contents - SDH, 20250106
    protected override bool CreateEffectPrefab()
    {
        bool isMade = false;
        if (attackCollider == null)
        {
            GameObject attackPrefab = GameObject.Instantiate(EffectPrefab);

            attackPrefab.transform.SetParent(Player.Instance.transform, false);
            Vector3 Scale = attackPrefab.transform.localScale;
            attackPrefab.transform.localScale = new Vector3(Scale.x * Player.Instance.GetMovingDirectionFloat(), Scale.y, Scale.z);

            attackCollider = attackPrefab.GetComponentInChildren<AttackCollider>();
            isMade = true;
        }
        else
        {
            Util.SetActive(attackCollider.gameObject, true);
        }

        attackCollider.SetAtk(damageMultiplier);
        return isMade;
    }

    protected abstract void UpdateKey();
}