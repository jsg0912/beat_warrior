public class Monster3Pattern : Pattern
{
    public Monster3Pattern()
    {
        Recognize = new RecognizeStrategyRanged();
        MoveNormal = new MoveStrategyFly();
        MoveChase = new MoveStrategyChase();
    }
}