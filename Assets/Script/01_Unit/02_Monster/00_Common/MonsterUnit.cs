public class MonsterUnit : Unit
{
    public PatternName patternName;
    public bool isKnockBackAble;
    public bool isTackleAble; // If it is true, then the monster can tackle the player.

    public MonsterUnit(MonsterInfo monsterInfo, UnitStat unitStat, PatternName patternName, bool isKnockBackAble = true, bool isTackleAble = false) : base(monsterInfo, unitStat)
    {
        this.patternName = patternName;
        this.isKnockBackAble = isKnockBackAble;
        this.isTackleAble = isTackleAble;
    }
}