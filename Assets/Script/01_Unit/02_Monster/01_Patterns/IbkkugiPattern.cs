public class IbkkugiPattern : Pattern
{
    public IbkkugiPattern()
    {
        Recognize = new RecognizeStrategyRanged();
        MoveNormal = new MoveStrategyNormal();
        MoveChase = new MoveStrategyChase();
        Attack = new SttackStrategyThrowIbkkugi();
    }
}