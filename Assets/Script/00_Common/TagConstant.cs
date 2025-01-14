using UnityEngine;

public static class TagConstant
{
    public const string Untagged = "Untagged";
    public const string Respawn = "Respawn";
    public const string Finish = "Finish";
    public const string EditorOnly = "EditorOnly";
    public const string MainCamera = "MainCamera";
    public const string Player = "Player";
    public const string GameController = "GameController";
    public const string Tile = "Tile";
    public const string Monster = "Monster";
    public const string Mark = "Mark";
    public const string Trap = "Trap";
    public const string Projectile = "Projectile";
    public const string Wall = "Wall";
    public const string Base = "Base";
    public const string Background = "Background";

    public static bool IsBlockTag(GameObject gameObject)
    {
        return gameObject.CompareTag(Base) || gameObject.CompareTag(Tile) || gameObject.CompareTag(Wall);
    }
}