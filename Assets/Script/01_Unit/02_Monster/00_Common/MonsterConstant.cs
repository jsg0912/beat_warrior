using System.Collections.Generic;
using UnityEngine;

public class MonsterConstant
{
    public const string walkAnimBool = "isWalk";
    public const string attackChargeAnimTrigger = "charge";
    public const string attackAnimTrigger = "attack";
    public const string attackEndAnimTrigger = "attack end";

    public const string turnAnimTrigger = "turn";

    public const string hurtAnimTrigger = "hurt";
    public const string dieAnimTrigger = "die";

    public const float moveSpeed = 1.0f;

    public const float RangedRecognizeRange = 10.0f;
    public const float MeleeRecognizeRange = 10.0f;

    public const float ThrowObjectXOffset = -0.5f;

    // Ibkkugi
    public const float IbkkugiThrowSpeed = 3f;
    public const float IbkkugiMaxHeight = 1.5f;

    // Dulduli
    public const float DulduliThrowSpeed = 3f;
    public const float DulduliMaxHeight = 1f;

    // Koppulso
    public const float KoppulsoRushSpeed = 10.0f;
    public const float KoppulsoRushDuration = 10.0f;

    public const float WallCheckRayDistance = 0.1f;
    public const float GroundCheckRayDistance = 0.1f;

    private static Dictionary<MonsterName, float> MoveSpeedRatio = new() {
        { MonsterName.Ippali, 1.1f },
        { MonsterName.Ibkkugi, 1.0f },
        { MonsterName.Koppulso, 1.6f },
        { MonsterName.Dulduli, 0.4f },
        { MonsterName.Giljjugi, 0.7f },
        { MonsterName.Itmomi, 0f }
    };

    public static Dictionary<MonsterName, float> MoveSpeed = new() {
        { MonsterName.Ippali, moveSpeed * MoveSpeedRatio[MonsterName.Ippali] },
        { MonsterName.Ibkkugi, moveSpeed * MoveSpeedRatio[MonsterName.Ibkkugi] },
        { MonsterName.Koppulso, moveSpeed * MoveSpeedRatio[MonsterName.Koppulso] },
        { MonsterName.Dulduli, moveSpeed * MoveSpeedRatio[MonsterName.Dulduli] },
        { MonsterName.Giljjugi, moveSpeed * MoveSpeedRatio[MonsterName.Giljjugi] },
        { MonsterName.Itmomi, moveSpeed * MoveSpeedRatio[MonsterName.Itmomi] },
    };

    public static Dictionary<MonsterName, float> AttackSpeed = new() {
        { MonsterName.Ippali, 2.0f },
        { MonsterName.Ibkkugi, 2.8f },
        { MonsterName.Koppulso, 7.0f },
        { MonsterName.Dulduli, 4.0f },
        { MonsterName.Giljjugi, 7.0f },
        { MonsterName.Itmomi, 5.0f }
    };

    public static Dictionary<MonsterName, float> AttackThrowSpeed = new() {
        { MonsterName.Ibkkugi, 0.5f },
        { MonsterName.Dulduli, 25.0f },
    };

    public static Dictionary<MonsterName, float> AttackStartDelays = new() {
        { MonsterName.Ippali, 1.0f },
        { MonsterName.Ibkkugi, 0.5f },
        { MonsterName.Koppulso, 0.5f },
        { MonsterName.Dulduli, 0.5f },
        { MonsterName.Giljjugi, 0.5f },
        { MonsterName.Itmomi, 0.5f }
    };

    public static Dictionary<MonsterName, float> AttackActionIntervals = new() {
        { MonsterName.Ippali, 0.5f },
        { MonsterName.Ibkkugi, 0.8f },
        { MonsterName.Koppulso, 2f },
        { MonsterName.Dulduli, 0.5f },
        { MonsterName.Giljjugi, 2.3f },
        { MonsterName.Itmomi, 0.5f }
    };
}