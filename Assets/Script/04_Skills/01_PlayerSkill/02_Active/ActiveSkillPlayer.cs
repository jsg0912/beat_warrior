using UnityEngine;

public abstract class ActiveSkillPlayer : ActiveSkill
{
    public PLAYERSKILLNAME skillName;
    protected KeyCode keyCode;
    protected PLAYERSTATUS status;

    protected override void UseSkill()
    {
        Player.Instance.SetPlayerStatus(status);
        SkillMethod();
    }

    public override void UpdateSkill()
    {
        base.UpdateSkill();
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