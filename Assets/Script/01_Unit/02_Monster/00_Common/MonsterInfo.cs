public class MonsterInfo : UnitInfo
{
    public MonsterName monsterName;

    public MonsterInfo(MonsterName monsterName, string description = null)
    {
        this.monsterName = monsterName;
        this.description = description;
    }
}