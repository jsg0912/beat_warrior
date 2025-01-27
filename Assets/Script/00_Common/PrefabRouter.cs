using System.Collections.Generic;

public class PrefabRouter
{
    public const string PlayerPrefabRoute = "Prefab/01_Unit/01_Player/";
    public const string ObjectPrefabRoute = "Prefab/02_Object/";
    public const string MonsterImageRoute = "Image/Monster/";
    public const string MiniMapUIPrefabRoute = "Prefab/03_UI/02_InGameUI/02_MiniMapUI/";

    public const string PlayerPrefab = PlayerPrefabRoute + "Player";
    public const string PlayerAttackPrefab = PlayerPrefabRoute + "AttackCollider";
    public const string Skill1Prefab = PlayerPrefabRoute + "Skill1Collider";
    public const string Skill2Prefab = PlayerPrefabRoute + "Skill2Collider";
    public const string GhostPrefab = PlayerPrefabRoute + "Ghost";

    public const string SoulPrefab = ObjectPrefabRoute + "Soul";
    public const string MarkerPrefab = ObjectPrefabRoute + "Marker";
    public const string ObjectPooler = ObjectPrefabRoute + "ObjectPooler";

    public const string UIPrefab = "Prefab/03_UI/UI";
    public const string MapMonsterIcon = MiniMapUIPrefabRoute + "MapMonsterIcon";
    public const string MiniMapIconIppali = MiniMapUIPrefabRoute + "MiniMapMonsterIconIppali";
    public const string MiniMapIconIbkkugi = MiniMapUIPrefabRoute + "MiniMapMonsterIconIbkkugi";
    public const string MiniMapIconKoppulso = MiniMapUIPrefabRoute + "MiniMapMonsterIconKoppulso";
    public const string IbkkugiThrow = "Prefab/02_Object/IbkkugiThrow";

    public const string EmptyImage = "Image/UI/empty";

    public static Dictionary<MonsterName, string> AttackPrefab = new() {
        { MonsterName.Ibkkugi, "Prefab/02_Object/IbkkugiThrow" },
        { MonsterName.Jiljili, "Prefab/02_Object/JiljiliThrow" },
        { MonsterName.Itmomi, "Prefab/02_Object/IsmomiThrow" },
        { MonsterName.Koppulso, "Prefab/02_Object/KoppulsoDashCollider" },
    };

    public static Dictionary<SkillName, string> TraitIconImages = new() {
        { SkillName.End, EmptyImage },
        { SkillName.AppendMaxHP, MonsterImageRoute + "Ippali/attack_0" },
        { SkillName.SkillReset, MonsterImageRoute + "Ippali/attack_2" },
        { SkillName.DoubleJump, MonsterImageRoute + "Ippali/attack_1" },
        { SkillName.Execution, MonsterImageRoute + "Ippali/attack_3" },
        { SkillName.AppendAttack, MonsterImageRoute + "Ippali/attack_4" },
        { SkillName.KillRecoveryHP, MonsterImageRoute + "Ippali/attack_5" },
    };
}