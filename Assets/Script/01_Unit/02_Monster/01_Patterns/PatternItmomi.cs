public class PatternItmomi : Pattern
{
    public PatternItmomi()
    {
        Recognize = new RecognizeStrategyRanged();
        Attack = new AttackStrategyThrowItmomi();
    }
}