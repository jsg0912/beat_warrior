using System.Collections.Generic;

public class PrefabRouter
{
    public const string PlayerPrefabRoute = "Prefab/01_Unit/01_Player/";
    public const string ObjectPrefabRoute = "Prefab/02_Object/";
    public const string PlayerObjectPrefabRoute = "Prefab/02_Object/04_Player/";
    public const string MonsterImageRoute = "Image/Monster/";
    public const string MiniMapMonsterIconRoute = "Prefab/03_UI/02_InGameUI/02_MiniMapUI/Silhouette/MiniMapMonsterIcon";
    public const string MonsterObjectRoute = "Prefab/02_Object/05_Monster/";
    public const string TraitIconRoute = "Image/UI/AltarUI/TraitIcon/";
    public const string InteractionObjectRoute = "Prefab/02_Object/03_InteractionObject/";
    public const string ScriptableObjectRoute = "Prefab/99_ScriptableObject/";
    public const string PlayerSkillEffectRoute = "Prefab/06_Effect/PlayerSkill/";
    public const string BossMonsterRoute = "Prefab/01_Unit/03_BossMonster/";

    public const string PlayerPrefab = PlayerPrefabRoute + "Player";
    public const string PlayerAttackPrefab = PlayerPrefabRoute + "AttackCollider";
    public const string Skill1Prefab = PlayerPrefabRoute + "Skill1Collider";
    public const string Skill2Prefab = PlayerPrefabRoute + "Skill2Collider";
    public const string GhostPrefab = PlayerPrefabRoute + "Ghost";
    public const string ReviveEffectPrefab = PlayerSkillEffectRoute + "ReviveEffect";

    public const string SoulPrefab = InteractionObjectRoute + "Soul";
    public const string MarkerPrefab = PlayerObjectPrefabRoute + "Marker";
    public const string ObjectPooler = ObjectPrefabRoute + "ObjectPooler";
    public const string SoundList = ScriptableObjectRoute + "SoundList";

    public const string UIPrefab = "Prefab/03_UI/UI";
    public const string IbkkugiThrow = "Prefab/02_Object/05_Monster/IbkkugiThrow";

    public const string EmptyImage = "Image/UI/empty";

    public static Dictionary<MonsterName, string> MonsterAttackPrefab = new() {
        { MonsterName.Ibkkugi, MonsterObjectRoute + "IbkkugiThrow"},
        { MonsterName.Itmomi, MonsterObjectRoute + "ItmomiThrow"},
        { MonsterName.Gurges, MonsterObjectRoute + "GurgesThrow"},
    };

    public static Dictionary<SkillName, string> TraitIconImages = new() {
        { SkillName.End, EmptyImage },
        { SkillName.AppendMaxHP, TraitIconRoute + "AppendMaxHP" },
        { SkillName.SkillReset, TraitIconRoute + "SkillReset" },
        { SkillName.DoubleJump, TraitIconRoute + "DoubleJump" },
        { SkillName.Execution, TraitIconRoute + "Execution" },
        { SkillName.AppendAttack, TraitIconRoute + "AppendAttack" },
        { SkillName.KillRecoveryHP, TraitIconRoute + "KillRecoveryHP" },
        { SkillName.Revive, TraitIconRoute + "Revive" },
    };

    public static string MiniMapIconRoute(MonsterName monsterName)
    {
        return MiniMapMonsterIconRoute + monsterName.ToString();
    }

    public const string TentaclePrefab = BossMonsterRoute + "Tentacle";
    public const string TentacleHorizontalPrefab = BossMonsterRoute + "TentacleHorizontal";
    public const string TentacleVerticalPrefab = BossMonsterRoute + "TentacleVertical";
    public const string IppaliEggPrefab = MonsterObjectRoute + "IppaliEgg";
    public const string IppaliPrefab = "Prefab/01_Unit/02_Monster/Ippali";


}