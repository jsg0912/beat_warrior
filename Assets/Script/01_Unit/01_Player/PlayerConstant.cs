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
    public const float moveSpeed = 5.6f;
    public const float jumpHeight = 18.0f;
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
    public const string attackRAnimTrigger = "attackR";
    public const string attackLAnimTrigger = "attackL";
    public const string markAnimTrigger = "mark";
    public const string dashAnimTrigger = "dash";
    public const string hurtAnimTrigger = "hurt";
    public const string skill1AnimTrigger = "skill1";
    public const string skill2AnimTrigger = "skill2";

    public const int MaxAdditionalSkillCount = 3;
}