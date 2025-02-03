using System.Collections.Generic;

public class PlayerConstant
{
    public static Dictionary<StatKind, int> defaultStat = new(){
        {StatKind.HP, 5},
        { StatKind.ATK, 1},
        { StatKind.Def, 0},
        { StatKind.JumpCount, 1},
        { StatKind.AttackCount, PlayerSkillConstant.attackCountMax}
    };

    public const float gravityScale = 5.0f;
    public const float moveSpeed = 7.0f;
    public const float jumpHeight = 20.0f;
    public const float playerHeight = 1.0f;

    public const float invincibilityTime = 0.5f;
    public const float knockBackedStunTime = 2.0f;
    public const float knockBackedDistance = 5.0f;

    public const string runAnimBool = "isRun";
    public const string jumpAnimTrigger = "jump";
    public const string fallAnimTrigger = "fall";
    public const string groundedAnimBool = "isGrounded";
    public const string dieAnimTrigger = "die";
    public const string restartAnimTrigger = "restart";
    public const string reviveAnimTrigger = "revive";

    public const int MaxAdditionalSkillCount = 3;
}