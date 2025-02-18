using UnityEngine;

public class ESkill : ActiveSkillPlayer
{
    private float dashRange;

    public ESkill(GameObject unit) : base(unit)
    {
        trigger = new() { PlayerConstant.ESkillAnimTrigger };

        damageMultiplier = PlayerSkillConstant.skill2Atk;
        dashRange = PlayerSkillConstant.skill2DashRange;

        EffectPrefab = Resources.Load(PrefabRouter.Skill2Prefab) as GameObject;
    }

    protected override void SetSkillName() { skillName = SkillName.Skill2; }

    protected override void UpdateKey()
    {
        keyCode = KeySetting.GetKey(PlayerAction.Skill2);
    }

    protected override void SetAttackCollider()
    {
        attackCollider = Player.Instance.colliderController.BodyAttackCollider;
        base.SetAttackCollider();
    }

    protected override void SkillMethod()
    {
        SetAttackCollider();

        Vector2 start = Player.Instance.transform.position;
        Vector2 end = start += new Vector2(dashRange, 0.0f) * Player.Instance.GetMovingDirectionFloat();

        Player.Instance.Dashing(end, false, true, false);
    }
}
