using System.Collections.Generic;

public class PlayerSkillConstant
{
    public const int attackCountMax = 2;
    public const float attackKnockBackDistance = 3.0f;
    public const float skill2DashRange = 5.0f;
    public const float skill2ColliderRadius = 0.65f;
    public const float DashEndPointInterval = 1f;
    public const float DashEndYOffset = 0.1f;
    public const float DashSpeed = 0.1f;
    public const float SkillResetProbability = 10.0f; // TODO: 임시로 테스트를 위해 90%로 함 - 신동환, 20240901

    public static Dictionary<SkillName, float> SkillCoolTime = new() {
        { SkillName.Attack, 4.0f },
        { SkillName.Mark, 8.0f },
        { SkillName.Dash, 8.0f },
        { SkillName.Skill1, 8.0f },
        { SkillName.Skill2, 8.0f },
        { SkillName.KillRecoveryHP, 10.0f}
    };

    public static List<SkillName> ResetSkillListByMarkKill = new() {
        SkillName.Mark,
        SkillName.Dash,
        SkillName.Skill1,
        SkillName.Skill2,
    };

    public static Dictionary<SkillName, PlayerSkillEffectColor> PlayerSkillEffectColorInfo = new()
    {
        { SkillName.Attack, PlayerSkillEffectColor.Yellow },
        { SkillName.Dash, PlayerSkillEffectColor.Purple },
        { SkillName.Skill1, PlayerSkillEffectColor.Purple },
        { SkillName.Skill2, PlayerSkillEffectColor.Purple },
    };

    public static Dictionary<AdditionalEffectName, bool> AdditionalEffectCanDuplicate = new() {
        { AdditionalEffectName.KnockBack, false },
    };

    public static List<MonsterName> MarkerIgnoreMonsterList = new()
    {
        MonsterName.Gurges,
    };

    public const float ghostDelayTimeMax = 0.05f;

    public const int attackAtk = 1;
    public const int dashAtk = 1;
    public const int skill1Atk = 1;
    public const int skill2Atk = 1;
    public const int KillRecoveryHPTrigger = 10;

    public const float markerSpeed = 25.0f;
    public const float markerDuration = 0.5f;
    public const float reviveDuration = 9.0f;
    public const float reviveEffectDuration = 6.0f;
}