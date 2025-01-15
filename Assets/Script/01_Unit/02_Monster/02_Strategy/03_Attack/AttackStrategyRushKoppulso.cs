using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackStrategyRushKoppulso : AttackStrategyRush
{
    protected override void SkillMethod()
    {
        base.SkillMethod();
        dashDuration = 3.0f;
        rushSpeed = 10.0f;

    }
}
