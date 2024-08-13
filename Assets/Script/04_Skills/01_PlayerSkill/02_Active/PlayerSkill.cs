using UnityEngine;

public abstract class PlayerSkill : Skill
{
    public PLAYERSKILLNAME skillName;

    protected PLAYERSTATUS status;

    protected override void UseSkill()
    {
        Player.Instance.SetPlayerStatus(status);

        SkillMethod();
    }

    protected override void CreateAttackPrefab()
    {
        GameObject attackPrefab = GameObject.Instantiate(AttackPrefab);
        attackPrefab.transform.SetParent(Player.Instance.transform, false);
        attackPrefab.GetComponent<AttackCollider>().SetAtk(atk);
    }
}