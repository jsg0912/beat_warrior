public class IbkkugiPattern : Pattern
{
    public IbkkugiPattern()
    {
        Recognize = new RecognizeStrategyRanged();
        MoveBasic = new MoveStrategyNormal();
        MoveChase = new MoveStrategyChase();
        Attack = new AttackStrategyThrow(MonsterConstant.IbkkugiThrowSpeed, MonsterConstant.IbkkugiMaxHeight);
    }
}