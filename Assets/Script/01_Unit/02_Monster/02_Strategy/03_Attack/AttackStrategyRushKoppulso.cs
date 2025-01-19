using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackStrategyRushKoppulso : AttackStrategyRush
{
    protected override void SkillMethod()
    {
        dashDuration = 10.0f;
        rushSpeed = 10.0f;
        base.SkillMethod();
    }
}
