using System.Collections.Generic;
using UnityEngine;

public class MonsterConstant
{
    public const float moveSpeed = 1.0f;
    public const float monsterDieRemoveTime = 3.0f;

    public const float RangedRecognizeRange = 10.0f;
    public const float MeleeRecognizeRange = 5.0f;

    public const float ThrowObjectXOffset = -0.5f;

    // Ibkkugi
    public const float IbkkugiThrowSpeed = 6f;
    public const float IbkkugiMaxHeight = 7f;

    // Dulduli
    public const float DulduliJumpPower = 5f;
    public const float DulduliJumpDuration = 0.7f;
    public static Vector2 DulduliSplashRange = new Vector2(3.0f, 1.0f);
    public const float DulduliGroggyDuration = 2.0f;

    // Koppulso
    public const float KoppulsoRushSpeed = 10.0f;
    public const float KoppulsoRushDuration = 4.0f;

    public const float WallCheckRayDistance = 0.1f;
    public const float GroundCheckRayDistance = 0.1f;

    // Boss Ch2

    public const int GergusIppaliSpawnCount = 2;

    public const float GergusThrowSpeed = 6f;
    public const float GergusMaxHeight = 7f;

    public static Dictionary<MonsterName, float> RecognizeRange = new() {
        { MonsterName.Ippali, 5.0f },
        { MonsterName.Ibkkugi, 10.0f },
        { MonsterName.Koppulso, 10.0f },
        { MonsterName.Dulduli, 7.0f },
        { MonsterName.Giljjugi, 7.0f },
        { MonsterName.Itmomi, 10.0f },
        { MonsterName.Gergus, 1000.0f }
    };

    public static Dictionary<MonsterName, float> AttackRange = new() {
        { MonsterName.Ippali, 1.0f },
        { MonsterName.Ibkkugi, 9.0f },
        { MonsterName.Koppulso, 10.0f },
        { MonsterName.Dulduli, 7.0f },
        { MonsterName.Giljjugi, 6.0f },
        { MonsterName.Itmomi, 9.0f },
        { MonsterName.Gergus, 1000.0f }
    };

    private static Dictionary<MonsterName, float> MoveSpeedRatio = new() {
        { MonsterName.Ippali, 2.0f },
        { MonsterName.Ibkkugi, 1.0f },
        { MonsterName.Koppulso, 1.6f },
        { MonsterName.Dulduli, 0.4f },
        { MonsterName.Giljjugi, 0.7f },
        { MonsterName.Itmomi, 0f },
        { MonsterName.Gergus, 0.0f }
    };

    public static Dictionary<MonsterName, float> MoveSpeed = new() {
        { MonsterName.Ippali, moveSpeed * MoveSpeedRatio[MonsterName.Ippali] },
        { MonsterName.Ibkkugi, moveSpeed * MoveSpeedRatio[MonsterName.Ibkkugi] },
        { MonsterName.Koppulso, moveSpeed * MoveSpeedRatio[MonsterName.Koppulso] },
        { MonsterName.Dulduli, moveSpeed * MoveSpeedRatio[MonsterName.Dulduli] },
        { MonsterName.Giljjugi, moveSpeed * MoveSpeedRatio[MonsterName.Giljjugi] },
        { MonsterName.Itmomi, moveSpeed * MoveSpeedRatio[MonsterName.Itmomi] },
        { MonsterName.Gergus, moveSpeed * MoveSpeedRatio[MonsterName.Gergus] },
    };

    public static Dictionary<MonsterName, float> AttackCoolTime = new() {
        { MonsterName.Ippali, 2.0f },
        { MonsterName.Ibkkugi, 2.2f },
        { MonsterName.Koppulso, 6.0f },
        { MonsterName.Dulduli, 3.0f },
        { MonsterName.Giljjugi, 6.0f },
        { MonsterName.Itmomi, 3.5f },
        { MonsterName.Gergus, 3.0f }
    };

    public static Dictionary<MonsterName, bool> IsRepeatAttackAnimation = new() {
        { MonsterName.TutorialIppali, false},
        { MonsterName.Ippali, false },
        { MonsterName.Ibkkugi, false },
        { MonsterName.Koppulso, true },
        { MonsterName.Dulduli, true },
        { MonsterName.Giljjugi, false },
        { MonsterName.Itmomi, false },
        { MonsterName.Gergus, false }
    };

    public static List<MonsterName> NotKnockBackAbleWhenAttacking = new(){
        MonsterName.Giljjugi, MonsterName.Koppulso, MonsterName.Dulduli, MonsterName.Gergus,
    };

    public static List<MonsterName> FixedAnimationWhenAttacking = new(){
        MonsterName.Giljjugi, MonsterName.Koppulso, MonsterName.Dulduli,
    };
}