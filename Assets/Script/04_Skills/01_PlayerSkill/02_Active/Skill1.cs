using UnityEngine;

public class Skill1 : ActiveSkillPlayer
{
    public override void Initialize()
    {
        skillName = PLAYERSKILLNAME.SKILL1;
        status = PLAYERSTATUS.SKILL1;

        damageMultiplier = PlayerSkillConstant.skill1Atk;

        coolTimeMax = PlayerSkillConstant.skill1CoolTimeMax;
        coolTime = 0;

        EffectPrefab = Resources.Load(PlayerSkillConstant.skill1Prefab) as GameObject;
    }

    protected override void UpdateKey()
    {
        keyCode = KeySetting.keys[ACTION.SKILL1];
    }

    protected override void SkillMethod()
    {
        CreateAttackPrefab();
    }
}
