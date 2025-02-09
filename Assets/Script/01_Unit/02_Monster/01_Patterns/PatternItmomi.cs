public class PatternItmomi : Pattern
{
    public PatternItmomi()
    {
        MoveBasic = new MoveStrategyFix();
        Recognize = new RecognizeStrategyRanged();
        Attack = new AttackStrategyThrowItmomi();
    }
}