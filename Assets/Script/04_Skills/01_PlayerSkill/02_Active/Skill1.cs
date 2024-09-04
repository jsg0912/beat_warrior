using UnityEngine;

public class Skill1 : ActiveSkillPlayer
{
    public Skill1(GameObject unit) : base(unit)
    {
        skillName = SkillName.Skill1;
        status = PlayerStatus.Skill1;

        damageMultiplier = PlayerSkillConstant.skill1Atk;

        coolTimeMax = PlayerSkillConstant.SkillCoolTime[skillName];
        coolTime = 0;

        EffectPrefab = Resources.Load(PlayerSkillConstant.skill1Prefab) as GameObject;
    }

    protected override void UpdateKey()
    {
        keyCode = KeySetting.keys[Action.Skill1];
    }

    protected override void SkillMethod()
    {
        CreateAttackPrefab();
    }
}
