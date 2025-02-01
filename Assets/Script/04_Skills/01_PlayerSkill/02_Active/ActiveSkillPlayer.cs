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
        Player.Instance.GetComponent<Rigidbody2D>().velocity = Vector2.zero; // TODO: 기획에 따라 스킬 사용 중 멈추는 것이 바뀔 수 있음 - SDH, 20250106

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
    protected override void CreateEffectPrefab()
    {
        if (attackCollider == null)
        {
            GameObject attackPrefab = GameObject.Instantiate(EffectPrefab);
            attackPrefab.transform.SetParent(Player.Instance.transform, false);
            attackCollider = attackPrefab.GetComponentInChildren<AttackCollider>();
        }

        if (!CheckObjectDirection())
        {
            Util.FlipLocalScaleX(attackCollider.gameObject);
        }

        attackCollider.SetAtk(damageMultiplier);
        foreach (AdditionalEffect additionalEffect in additionalEffects) attackCollider.SetAdditionalEffect(additionalEffect);
    }

    protected abstract void UpdateKey();

    public bool CheckObjectDirection()
    {
        if (Player.Instance.GetMovingDirectionFloat() * attackCollider.transform.localScale.x > 0) return true;

        return false;
    }
}