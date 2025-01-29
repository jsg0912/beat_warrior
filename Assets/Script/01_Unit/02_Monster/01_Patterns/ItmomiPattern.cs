public class ItmomiPattern : Pattern
{
    public ItmomiPattern()
    {
        MoveBasic = new MoveStrategyRandom();
        Recognize = new RecognizeStrategyRanged();
        Attack = new AttackStrategyThrowItmomi();
    }
}