public class ItmomiPattern : Pattern
{
    public ItmomiPattern()
    {
        Recognize = new RecognizeStrategyRanged();
        Attack = new AttackStrategyThrowItmomi();
    }
}