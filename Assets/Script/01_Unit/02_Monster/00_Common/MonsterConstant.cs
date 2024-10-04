using System.Collections.Generic;
using UnityEngine;

public class MonsterConstant
{
    public const string walkAnimBool = "isWalk";
    public const string attackAnimTrigger = "attack";
    public const string hurtAnimTrigger = "hurt";
    public const string dieAnimTrigger = "die";

    public const float moveSpeed = 1.0f;

    public const string GroundLayer = "Tile";
    public const string PlayerLayer = "Player";
    public const float RangedRecognizeRange = 10.0f;
    public const float MeleeRecognizeRange = 10.0f;

    public const float IbkkugiMaxHeight = 1f;
    public const float ThrowObjectYOffset = 0.5f;


    public static Dictionary<MonsterName, float> MoveSpeedRatio = new() {
        { MonsterName.Ippali, 1.1f },
        { MonsterName.Ibkkugi, 1.0f },
        { MonsterName.Koppulso, 1.6f },
        { MonsterName.Jiljili, 0.4f },
        { MonsterName.Giljjugi, 0.7f },
        { MonsterName.Itmomi, 1.2f }
    };

    public static Dictionary<MonsterName, float> MoveSpeed = new() {
        { MonsterName.Ippali, moveSpeed * MoveSpeedRatio[MonsterName.Ippali] },
        { MonsterName.Ibkkugi, moveSpeed * MoveSpeedRatio[MonsterName.Ibkkugi] },
        { MonsterName.Koppulso, moveSpeed * MoveSpeedRatio[MonsterName.Koppulso] },
        { MonsterName.Jiljili, moveSpeed * MoveSpeedRatio[MonsterName.Jiljili] },
        { MonsterName.Giljjugi, moveSpeed * MoveSpeedRatio[MonsterName.Giljjugi] },
        { MonsterName.Itmomi, moveSpeed * MoveSpeedRatio[MonsterName.Itmomi] },
    };

    public static Dictionary<MonsterName, float> AttackSpeed = new() {
        { MonsterName.Ippali, 2.0f },
        { MonsterName.Ibkkugi, 2.8f },
        { MonsterName.Koppulso, 3.0f },
        { MonsterName.Jiljili, 4.0f },
        { MonsterName.Giljjugi, 7.0f },
        { MonsterName.Itmomi, 1.2f }
    };

    public static Dictionary<MonsterName, float> AttackThrowSpeed = new() {
        { MonsterName.Ibkkugi, 0.5f },
        { MonsterName.Jiljili, 25.0f },
    };

    public static Dictionary<MonsterName, float> AttackDelay = new() {
        { MonsterName.Ippali, 0.5f },
        { MonsterName.Ibkkugi, 0.5f },
        { MonsterName.Koppulso, 0.5f },
        { MonsterName.Jiljili, 0.5f },
        { MonsterName.Giljjugi, 0.5f },
        { MonsterName.Itmomi, 0.5f }
    };

    public static Dictionary<MonsterName, float> AnimationDelay = new() {
        { MonsterName.Ippali, 0.5f },
        { MonsterName.Ibkkugi, 0.5f },
        { MonsterName.Koppulso, 0.5f },
        { MonsterName.Jiljili, 0.5f },
        { MonsterName.Giljjugi, 0.5f },
        { MonsterName.Itmomi, 0.5f }
    };
}