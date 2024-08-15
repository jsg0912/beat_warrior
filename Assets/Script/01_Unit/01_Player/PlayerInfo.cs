public class PlayerInfo : UnitInfo
{
    public string playerName; // User's Nickname

    public PlayerInfo(string playerName, string description = null)
    {
        this.playerName = playerName;
        this.description = description;
    }
}