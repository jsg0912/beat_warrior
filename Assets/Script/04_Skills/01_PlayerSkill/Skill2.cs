using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill2 : Skill
{
    void Start()
    {
        status = PLAYERSTATUS.SKILL2;

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
