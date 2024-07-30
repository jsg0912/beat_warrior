using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill1 : Skill
{
    void Start()
    {
        status = PLAYERSTATUS.SKILL1;

        animTrigger = PlayerSkillConstant.skill1AnimTrigger;

        cooltimeMax = PlayerSkillConstant.skill1CoolTimeMax;
        cooltime = 0;
    }

    protected override void UpdateKey()
    {
        key = KeySetting.keys[ACTION.SKILL1];
    }

    protected override void SkillMethod()
    {

    }
}
