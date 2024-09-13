public class Monster2Pattern : Pattern
{
    public Monster2Pattern()
    {
        Recognize = new RecognizeStrategyRanged();
        MoveNormal = new MoveStrategyNormal();
        MoveChase = new MoveStrategyChase();
    }
}
