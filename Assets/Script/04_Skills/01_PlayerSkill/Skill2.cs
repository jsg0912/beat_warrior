using UnityEngine;

public class Skill2 : PlayerSkill
{
    public override void Initialize()
    {
        skillName = PLAYERSKILLNAME.SKILL2;
        status = PLAYERSTATUS.SKILL2;
        animTrigger = PlayerSkillConstant.skill2AnimTrigger;

        atk = PlayerSkillConstant.skill2Atk;

        cooltimeMax = PlayerSkillConstant.skill2CoolTimeMax;
        cooltime = 0;

        AttackPrefab = Resources.Load(PlayerSkillConstant.skill2Prefab) as GameObject;
    }

    protected override void UpdateKey()
    {
        key = KeySetting.keys[ACTION.SKILL2];
    }

    protected override void SkillMethod()
    {
        CreateAttackPrefab();
    }
}
