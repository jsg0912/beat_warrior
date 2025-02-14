public class MonsterUnit : Unit
{
    public PatternName patternName;
    public bool isKnockBackAble;
    private bool isKnockBackAbleDefault;

    public MonsterUnit(MonsterInfo monsterInfo, UnitStat unitStat, PatternName patternName, bool isKnockBackAble = true) : base(monsterInfo, unitStat)
    {
        this.patternName = patternName;
        this.isKnockBackAble = isKnockBackAble;
        isKnockBackAbleDefault = isKnockBackAble;
    }

    public void ResetIsKnockBackAble()
    {
        isKnockBackAble = isKnockBackAbleDefault;
    }
}