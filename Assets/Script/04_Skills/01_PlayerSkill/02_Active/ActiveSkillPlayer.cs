using UnityEngine;

public abstract class ActiveSkillPlayer : ActiveSkill
{
    protected KeyCode keyCode;
    protected PlayerStatus status;
    protected PlayerSkillColliderController attackCollider; // TODO: if there are different type of active skill without "attack", then we have to divide this function's contents - SDH, 20250106

    protected ActiveSkillPlayer(GameObject unit, SkillTier skillTier) : base(unit, skillTier) { }

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

            attackCollider = attackPrefab.GetComponent<PlayerSkillColliderController>();
            isMade = true;
        }
        else
        {
            Util.SetActive(attackCollider.gameObject, true);
        }

        attackCollider.SetAtk(damageMultiplier);
        Vector3 Scale = attackCollider.gameObject.transform.localScale;
        if (Player.Instance.GetMovingDirectionFloat() * Scale.x < 0)
        {
            attackCollider.gameObject.transform.localScale = new Vector3(-Scale.x, Scale.y, Scale.z);
        }
        return isMade;
    }

    protected abstract void UpdateKey();
}