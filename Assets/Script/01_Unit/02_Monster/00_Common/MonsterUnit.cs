
public class MonsterUnit : Unit
{
    public Pattern pattern;

    public MonsterUnit(MonsterInfo monsterInfo, UnitStat unitStat, Pattern pattern) : base(monsterInfo, unitStat)
    {
        this.pattern = pattern;
    }
}