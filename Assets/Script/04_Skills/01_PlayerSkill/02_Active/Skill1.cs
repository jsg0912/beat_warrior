using UnityEngine;

public class Skill1 : PlayerSkill
{
    public override void Initialize()
    {
        skillName = PLAYERSKILLNAME.SKILL1;
        status = PLAYERSTATUS.SKILL1;
        animTrigger = PlayerSkillConstant.skill1AnimTrigger;

        atk = PlayerSkillConstant.skill1Atk;

        cooltimeMax = PlayerSkillConstant.skill1CoolTimeMax;
        cooltime = 0;

        AttackPrefab = Resources.Load(PlayerSkillConstant.skill1Prefab) as GameObject;
    }

    protected override void UpdateKey()
    {
        key = KeySetting.keys[ACTION.SKILL1];
    }

    protected override void SkillMethod()
    {
        CreateAttackPrefab();
    }
}
