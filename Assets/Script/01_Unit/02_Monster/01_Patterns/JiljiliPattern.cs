public class JiljiliPattern : Pattern
{
    public JiljiliPattern()
    {
        Recognize = new RecognizeStrategyMelee();
        MoveBasic = new MoveStrategyNormal();
        MoveChase = new MoveStrategyChase();
        Attack = new AttackStrategyThrow();
    }
}