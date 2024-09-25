using System.Collections.Generic;

public class PrefabRouter
{
    public const string PlayerAttackPrefab = "Prefab/01_Unit/01_Player/AttackCollider";
    public const string Skill1Prefab = "Prefab/01_Unit/01_Player/Skill1Collider";
    public const string Skill2Prefab = "Prefab/01_Unit/01_Player/Skill2Collider";
    public const string SoulPrefab = "Prefab/02_Object/Soul";
    public const string GhostPrefab = "Prefab/01_Unit/01_Player/Ghost";
    public const string MarkerPrefab = "Prefab/02_Object/Marker";

    public static Dictionary<MonsterName, string> AttackPrefab = new() {
        { MonsterName.Ibkkugi, "Prefab/02_Object/IbkkugiThrow" },
        { MonsterName.Jiljili, "Prefabs/02_Object/JiljiliThrow" },
        { MonsterName.Ismomi, "Prefabs/dd" },
    };
}