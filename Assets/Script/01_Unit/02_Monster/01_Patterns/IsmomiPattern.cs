public class IsmomiPattern : Pattern
{
    public IsmomiPattern()
    {
        Recognize = new RecognizeStrategyRanged();
        Attack = new AttackStrategyThrowIsmomi();
    }
}