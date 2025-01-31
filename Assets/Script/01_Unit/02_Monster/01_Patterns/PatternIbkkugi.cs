public class PatternIbkkugi : Pattern
{
    public PatternIbkkugi()
    {
        Recognize = new RecognizeStrategyRanged();
        MoveBasic = new MoveStrategyNormal();
        MoveChase = new MoveStrategyChase();
        Attack = new AttackStrategyThrow(MonsterConstant.IbkkugiThrowSpeed, MonsterConstant.IbkkugiMaxHeight);
    }
}