using System.Collections.Generic;
using UnityEngine;

public class MonsterConstant
{
    public const string walkAnimBool = "isWalk";
    public const string attackAnimTrigger = "attack";
    public const string hurtAnimTrigger = "hurt";
    public const string dieAnimTrigger = "die";

    public const float moveSpeed = 1.0f;

    public const string PlayerLayer = "Player";
    public const string GroundLayer = "Tile";
    public const float RangedRecognizeRange = 5.0f;
    public const float MeleeRecognizeRange = 5.0f;

    public static Dictionary<MonsterName, float> MoveSpeedRatio = new() {
        { MonsterName.Monster1, 1.1f },
        { MonsterName.Monster2, 1.0f },
        { MonsterName.Monster3, 1.6f },
        { MonsterName.Monster4, 0.4f },
        { MonsterName.Monster5, 0.7f },
        { MonsterName.Monster6, 1.2f }
    };

    public static Dictionary<MonsterName, float> MoveSpeed = new() {
        { MonsterName.Monster1, moveSpeed * MoveSpeedRatio[MonsterName.Monster1] },
        { MonsterName.Monster2, moveSpeed * MoveSpeedRatio[MonsterName.Monster2] },
        { MonsterName.Monster3, moveSpeed * MoveSpeedRatio[MonsterName.Monster3] },
        { MonsterName.Monster4, moveSpeed * MoveSpeedRatio[MonsterName.Monster4] },
        { MonsterName.Monster5, moveSpeed * MoveSpeedRatio[MonsterName.Monster5] },
        { MonsterName.Monster6, moveSpeed * MoveSpeedRatio[MonsterName.Monster6] },
    };

    public static Dictionary<MonsterName, float> AttackSpeed = new() {
        { MonsterName.Monster1, 2.0f },
        { MonsterName.Monster2, 2.8f },
        { MonsterName.Monster3, 3.0f },
        { MonsterName.Monster4, 4.0f },
        { MonsterName.Monster5, 7.0f },
        { MonsterName.Monster6, 1.2f }
    };
}