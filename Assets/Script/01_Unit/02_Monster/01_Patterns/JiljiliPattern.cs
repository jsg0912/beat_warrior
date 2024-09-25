public class JiljiliPattern : Pattern
{
    public JiljiliPattern()
    {
        Recognize = new RecognizeStrategyRanged();
        MoveNormal = new MoveStrategyNormal();
        MoveChase = new MoveStrategyChase();
        Attack = new AttackStrategyThrow();
    }
}