public class PatternItmomi : Pattern
{
    public PatternItmomi()
    {
        MoveBasic = new MoveStrategyFix();
        MoveChase = new MoveStrategyChaseFix();
        Recognize = new RecognizeStrategyRanged();
        Attack = new AttackStrategyThrowItmomi();
    }
}