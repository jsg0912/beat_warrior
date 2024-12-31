public class IsmomiPattern : Pattern
{
    public IsmomiPattern()
    {
        Recognize = new RecognizeStrategyRanged();
        MoveNormal = new MoveStrategyNormal();
        MoveChase = new MoveStrategyChase();
        Attack = new SttackStrategyThrowIsmomi();
    }
}