public enum PLAYERSTATUS
{
    IDLE,
    RUN,
    JUMP,
    MARK,
    DASH,
    ATTACK,
    SKILL1,
    SKILL2,
    DEAD,
    NULL
}

public class PlayerConstant
{
    public const int hpMax = 3;

    public const float gravityScale = 5.0f;
    public const float moveSpeed = 7.0f;
    public const float jumpHeight = 20.0f;

    public const float invincibilityTime = 0.5f;
}