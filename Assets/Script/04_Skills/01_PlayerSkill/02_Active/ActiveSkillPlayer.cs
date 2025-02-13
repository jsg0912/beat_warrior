using System.Collections.Generic;
using UnityEngine;

public abstract class ActiveSkillPlayer : ActiveSkill
{
    protected KeyCode keyCode;
    protected List<string> trigger;
    protected PlayerSkillEffectColor effectColor;
    protected AttackCollider attackCollider; // TODO: if there are different type of active skill without "attack", then we have to divide this function's contents - SDH, 20250106

    protected ActiveSkillPlayer(GameObject unit) : base(unit)
    {
        if (PlayerSkillConstant.PlayerSkillEffectColorInfo.ContainsKey(skillName)) effectColor = PlayerSkillConstant.PlayerSkillEffectColorInfo[skillName];
        else effectColor = PlayerSkillEffectColor.None;
    }

    protected override void UseSkill()
    {
        if (trigger?.Count > 0) Player.Instance.SetAnimTrigger(trigger[Random.Range(0, trigger.Count)]);
        if (effectColor != PlayerSkillEffectColor.None) Player.Instance.SetLastSkillColor(effectColor);

        SkillMethod();

        if (Player.Instance.useSKillFuncList != null) Player.Instance.useSKillFuncList(this);
    }

    // TODO: Game System 적 기능이라, CommandManager나 기타 다른 클래스로 옮겨가야 함.
    public virtual void CheckInputKeyCode()
    {
        UpdateKey();

        if (Input.GetKeyDown(keyCode))
        {
            if (Player.Instance.IsActionAble()) TrySkill();
        }
    }

    public virtual void CheckFixedInputKeyCode() { }

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

        attackCollider.SetAtk(damageMultiplier * Player.Instance.GetFinalStat(StatKind.ATK));
        foreach (AdditionalEffect additionalEffect in additionalEffects) attackCollider.SetAdditionalEffect(additionalEffect);
    }

    protected abstract void UpdateKey();

    public bool CheckObjectDirection()
    {
        if (Player.Instance.GetMovingDirectionFloat() * attackCollider.transform.localScale.x > 0) return true;

        return false;
    }
}