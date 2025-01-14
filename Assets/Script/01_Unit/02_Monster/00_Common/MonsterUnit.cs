public class MonsterUnit : Unit
{
    public PatternName patternName;

    public MonsterUnit(MonsterInfo monsterInfo, UnitStat unitStat, PatternName patternName) : base(monsterInfo, unitStat)
    {
        this.patternName = patternName;
    }
}