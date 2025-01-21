public class MonsterUnit : Unit
{
    public PatternName patternName;
    public bool isKnockBackAble;
    private bool isKnockBackAbleDefault;
    public bool isTackleAble; // If it is true, then the monster can tackle the player.
    private bool isTackleAbleDefault;

    public MonsterUnit(MonsterInfo monsterInfo, UnitStat unitStat, PatternName patternName, bool isKnockBackAble = true, bool isTackleAble = false) : base(monsterInfo, unitStat)
    {
        this.patternName = patternName;
        this.isKnockBackAble = isKnockBackAble;
        isKnockBackAbleDefault = isKnockBackAble;
        this.isTackleAble = isTackleAble;
        isTackleAbleDefault = isTackleAble;
    }

    public void ResetIsKnockBackAble()
    {
        isKnockBackAble = isKnockBackAbleDefault;
    }
    public void ResetIsTackleAble()
    {
        isTackleAble = isTackleAbleDefault;
    }
}