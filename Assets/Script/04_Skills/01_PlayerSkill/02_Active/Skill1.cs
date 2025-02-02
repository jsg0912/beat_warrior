using UnityEngine;

public class Skill1 : ActiveSkillPlayer
{
    public Skill1(GameObject unit) : base(unit)
    {
        status = PlayerStatus.Skill1;

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
