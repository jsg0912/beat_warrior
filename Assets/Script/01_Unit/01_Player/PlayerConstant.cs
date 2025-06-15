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

    public const Direction initDirection = Direction.Left;

    public const float gravityScale = 5.0f;
    public const float moveSpeed = 5.6f;
    public const float jumpPower = 20.0f;
    public const float playerHeight = 1.0f;

    public const float invincibilityTime = 1.0f;
    public const float knockBackedStunTime = 2.0f;
    public const float knockBackedDistance = 5.0f;

    public const string runAnimBool = "isRun";
    public const string jumpAnimTrigger = "jump";
    public const string isAttackingAnimBool = "isAttacking";
    public const string groundedAnimBool = "isGrounded";
    public const string dieAnimTrigger = "die";
    public const string restartAnimTrigger = "restart";
    public const string reviveAnimTrigger = "revive";
    public const string attackAnimTrigger = "attack";
    public const string dashAnimTrigger = "dash";
    public const string dashEndAnimBool = "DashEnd";
    public const string hurtAnimTrigger = "hurt";
    public const string QSkill1AnimTrigger = "qSkill1";
    public const string QSkill2AnimTrigger = "qSkill2";
    public const string ESkillAnimTrigger = "eSkill";
    public const string Rest1AnimTrigger = "rest1";
    public const string Rest2AnimTrigger = "rest2";

    public const int MaxAdditionalSkillCount = 3;
    public const int PlayerHurtFaceTriggerHp = 1;
}