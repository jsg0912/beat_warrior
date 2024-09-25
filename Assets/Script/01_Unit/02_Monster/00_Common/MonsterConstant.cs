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
    public const float RangedRecognizeRange = 5.0f;
    public const float MeleeRecognizeRange = 5.0f;

    public static Dictionary<MonsterName, float> MoveSpeedRatio = new() {
        { MonsterName.Ippali, 1.1f },
        { MonsterName.Ibkkugi, 1.0f },
        { MonsterName.Koppulso, 1.6f },
        { MonsterName.Jiljili, 0.4f },
        { MonsterName.Giljjugi, 0.7f },
        { MonsterName.Ismomi, 1.2f }
    };

    public static Dictionary<MonsterName, float> MoveSpeed = new() {
        { MonsterName.Ippali, moveSpeed * MoveSpeedRatio[MonsterName.Ippali] },
        { MonsterName.Ibkkugi, moveSpeed * MoveSpeedRatio[MonsterName.Ibkkugi] },
        { MonsterName.Koppulso, moveSpeed * MoveSpeedRatio[MonsterName.Koppulso] },
        { MonsterName.Jiljili, moveSpeed * MoveSpeedRatio[MonsterName.Jiljili] },
        { MonsterName.Giljjugi, moveSpeed * MoveSpeedRatio[MonsterName.Giljjugi] },
        { MonsterName.Ismomi, moveSpeed * MoveSpeedRatio[MonsterName.Ismomi] },
    };

    public static Dictionary<MonsterName, float> AttackSpeed = new() {
        { MonsterName.Ippali, 2.0f },
        { MonsterName.Ibkkugi, 2.8f },
        { MonsterName.Koppulso, 3.0f },
        { MonsterName.Jiljili, 4.0f },
        { MonsterName.Giljjugi, 7.0f },
        { MonsterName.Ismomi, 1.2f }
    };

    public static Dictionary<MonsterName, string> AttackPrefab = new() {
        { MonsterName.Ibkkugi, "Prefabs/dd" },
        { MonsterName.Jiljili, "Prefabs/dd" },
        { MonsterName.Ismomi, "Prefabs/dd" },
    };
}