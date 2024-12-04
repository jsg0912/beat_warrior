public class JiljiliPattern : Pattern
{
    public JiljiliPattern()
    {
        Recognize = new RecognizeStrategyMelee();
        MoveNormal = new MoveStrategyNormal();
        MoveChase = new MoveStrategyChase();
        Attack = new AttackStrategyThrow();
    }
}