using System.Collections.Generic;

public class PlayerSkillConstant
{
    public const float SkillDelayInterval = 0.2f;

    public const int attackCountMax = 2;
    public const float attackKnockBackRange = 3.0f;
    public const float skill2DashRange = 2.0f;
    public const float DashEndPointInterval = 1f;
    public const float DashSpeed = 0.03f;
    public const float SkillResetProbability = 0.9f; // TODO: 임시로 테스트를 위해 50%로 함 - 신동환, 20240901

    public static Dictionary<SkillName, float> SkillCoolTime = new() {
        { SkillName.Attack, 4.0f },
        { SkillName.Mark, 8.0f },
        { SkillName.Dash, 8.0f },
        { SkillName.Skill1, 8.0f },
        { SkillName.Skill2, 8.0f }
    };

    public const float recoveryHPTimeMax = 10.0f;
    public const float ghostDelayTimeMax = 0.01f;

    public const int attackAtk = 1;
    public const int dashAtk = 1;
    public const int skill1Atk = 1;
    public const int skill2Atk = 1;
    public const int KillRecoveryHPTrigger = 10;

    public const string attackAnimTrigger = "attack";
    public const string markAnimTrigger = "mark";
    public const string dashAnimTrigger = "dash";
    public const string skill1AnimTrigger = "skill1";
    public const string skill2AnimTrigger = "skill2";

    public const string attackPrefab = "Prefab/AttackCollider";
    public const string skill1Prefab = "Prefab/Skill1Collider";
    public const string skill2Prefab = "Prefab/Skill2Collider";
}