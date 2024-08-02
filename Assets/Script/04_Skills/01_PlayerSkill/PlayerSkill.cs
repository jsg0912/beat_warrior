using UnityEngine;

public abstract class PlayerSkill : Skill
{
    public PLAYERSKILLNAME skillName;

    protected PLAYERSTATUS status;

    protected override void UseSkill()
    {
        Player.Instance.SetPlayerStatus(status);
        Player.Instance.SetPlayerAnimTrigger(animTrigger);

        SkillMethod();
    }

    protected override void CreateAttackPrefab()
    {
        GameObject sword = GameObject.Instantiate(AttackPrefab);
        sword.transform.SetParent(Player.Instance.transform, false);
        sword.GetComponent<AttackCollider>().SetAtk(atk);
    }
}