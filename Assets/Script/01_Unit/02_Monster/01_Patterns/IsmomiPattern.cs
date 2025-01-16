public class IsmomiPattern : Pattern
{
    public IsmomiPattern()
    {
        Recognize = new RecognizeStrategyRanged();
        MoveBasic = new MoveStrategyNormal();
        MoveChase = new MoveStrategyChase();
        Attack = new AttackStrategyThrowIsmomi();
    }
}