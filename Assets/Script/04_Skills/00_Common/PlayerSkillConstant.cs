public enum PLAYERSKILLNAME
{
    ATTACK,
    MARK,
    DASH,
    SKILL1,
    SKILL2,
    NULL
}


public class PlayerSkillConstant
{
    public const int attackPointMax = 2;

    public const float attackChargeTimeMax = 10.0f;
    public const float markCoolTimeMax = 2.0f;
    public const float dashCoolTimeMax = 2.0f;
    public const float skill1CoolTimeMax = 2.0f;
    public const float skill2CoolTimeMax = 2.0f;
    public const float ghostDelayTimeMax = 0.05f;

    public const string attackAnimTrigger = "attack";
    public const string markAnimTrigger = "mark";
    public const string dashAnimTrigger = "dash";
    public const string skill1AnimTrigger = "skill1";
    public const string skill2AnimTrigger = "skill2";
}