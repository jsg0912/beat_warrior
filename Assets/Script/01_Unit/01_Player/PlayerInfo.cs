public class PlayerInfo : UnitInfo
{
    public string playerName;

    public PlayerInfo(string playerName, string description = null)
    {
        this.playerName = playerName;
        this.description = description;
    }
}