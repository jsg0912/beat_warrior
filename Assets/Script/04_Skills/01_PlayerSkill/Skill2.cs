using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill2 : Skill
{
    public override void Initialize()
    {
        skillName = PLAYERSKILLNAME.SKILL2;
        status = PLAYERSTATUS.SKILL2;
        animTrigger = PlayerSkillConstant.skill2AnimTrigger;

        cooltimeMax = PlayerSkillConstant.skill2CoolTimeMax;
        cooltime = 0;
    }

    protected override void UpdateKey()
    {
        key = KeySetting.keys[ACTION.SKILL2];
    }

    protected override void SkillMethod()
    {

    }
}
