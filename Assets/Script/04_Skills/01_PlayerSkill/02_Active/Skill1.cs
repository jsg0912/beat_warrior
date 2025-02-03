using UnityEngine;

public class Skill1 : ActiveSkillPlayer
{
    public Skill1(GameObject unit) : base(unit)
    {
        trigger = new() { PlayerConstant.skill1LAnimTrigger, PlayerConstant.skill1RAnimTrigger };

        damageMultiplier = PlayerSkillConstant.skill1Atk;
        EffectPrefab = Resources.Load(PrefabRouter.Skill1Prefab) as GameObject;
    }

    protected override void SetSkillName() { skillName = SkillName.Skill1; }

    protected override void UpdateKey()
    {
        keyCode = KeySetting.keys[PlayerAction.Skill1];
    }

    protected override void SkillMethod()
    {
        CreateEffectPrefab();
    }

    protected override void CreateEffectPrefab()
    {
        attackCollider = Player.Instance.colliderController.shortBladeCollider;
        base.CreateEffectPrefab();
    }
}
