public class Monster2Pattern : Pattern
{
    public Monster2Pattern()
    {
        Recognize = new RecognizeStrategyRanged();
        MoveBasic = new MoveStrategyNormal();
        MoveChase = new MoveStrategyChase();
    }
}