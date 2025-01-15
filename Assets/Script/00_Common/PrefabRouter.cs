using System.Collections.Generic;

public class PrefabRouter
{
    public const string PlayerPrefab = "Prefab/01_Unit/01_Player/Player";

    public const string PlayerAttackPrefab = "Prefab/01_Unit/01_Player/AttackCollider";
    public const string Skill1Prefab = "Prefab/01_Unit/01_Player/Skill1Collider";
    public const string Skill2Prefab = "Prefab/01_Unit/01_Player/Skill2Collider";
    public const string SoulPrefab = "Prefab/02_Object/Soul";
    public const string GhostPrefab = "Prefab/01_Unit/01_Player/Ghost";
    public const string MarkerPrefab = "Prefab/02_Object/Marker";
    public const string UIPrefab = "Prefab/03_UI/UI";
    public const string ObjectPooler = "Prefab/02_Object/ObjectPooler";
    public const string MapMonsterIcon = "Prefab/03_UI/MapMonsterIcon";
    public const string IbkkugiThrow = "Prefab/02_Object/IbkkugiThrow";

    public static Dictionary<MonsterName, string> AttackPrefab = new() {
        { MonsterName.Ibkkugi, "Prefab/02_Object/IbkkugiThrow" },
        { MonsterName.Jiljili, "Prefab/02_Object/JiljiliThrow" },
        { MonsterName.Itmomi, "Prefab/02_Object/IsmomiThrow" },
        { MonsterName.Koppulso, "Prefab/02_Object/KoppulsoDashCollider" },
    };
}