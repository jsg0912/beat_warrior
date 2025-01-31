public class PatternItmomi : Pattern
{
    public PatternItmomi()
    {
        MoveBasic = new MoveStrategyRandom();
        Recognize = new RecognizeStrategyRanged();
        Attack = new AttackStrategyThrowItmomi();
    }
}