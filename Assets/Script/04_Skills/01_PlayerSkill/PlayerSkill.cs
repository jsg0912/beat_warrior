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
        // TODO: sword를 생성한다가 모든 PlayerSkill의 공통 구조가 아니기 때문에, 여기 있으면 안됨 - Tony, 2024.08.02
        GameObject sword = GameObject.Instantiate(AttackPrefab);
        sword.transform.SetParent(Player.Instance.transform, false);
        sword.GetComponent<AttackCollider>().SetAtk(atk);
    }
}