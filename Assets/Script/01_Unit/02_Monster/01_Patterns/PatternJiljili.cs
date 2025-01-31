public class PatternJiljili : Pattern
{
    public PatternJiljili()
    {
        Recognize = new RecognizeStrategyMelee();
        MoveBasic = new MoveStrategyNormal();
        MoveChase = new MoveStrategyChase();
        Attack = new AttackStrategyThrow(MonsterConstant.JiljiliThrowSpeed, MonsterConstant.JiljiliMaxHeight);
    }
}