using UnityEngine;

public class QSkill : ActiveSkillPlayer
{
    public QSkill(GameObject unit) : base(unit)
    {
        trigger = new() { PlayerConstant.QSkill1AnimTrigger, PlayerConstant.QSkill2AnimTrigger };

        damageMultiplier = PlayerSkillConstant.skill1Atk;
        EffectPrefab = Resources.Load(PrefabRouter.Skill1Prefab) as GameObject;
    }

    protected override void SetSkillName() { skillName = SkillName.Skill1; }

    protected override void UpdateKey()
    {
        keyCode = KeySetting.GetKey(PlayerAction.Skill1);
    }

    protected override void SkillMethod()
    {
        SetAttackCollider();
    }

    protected override void SetAttackCollider()
    {
        attackCollider = Player.Instance.colliderController.shortBladeCollider;
        base.SetAttackCollider();
    }
}
