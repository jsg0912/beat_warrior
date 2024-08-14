using UnityEngine;

public abstract class ActiveSkillPlayer : ActiveSkill
{
    protected KeyCode keyCode;
    protected PlayerStatus status;

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

            if (Player.Instance.HaveTrait(SkillName.SkillReset) != null)
            {
                if (Random.Range(0, 10) == 0) coolTime = 0;
            }
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