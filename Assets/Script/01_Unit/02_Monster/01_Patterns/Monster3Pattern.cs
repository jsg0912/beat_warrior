public class Monster3Pattern : Pattern
{
    public Monster3Pattern()
    {
        Recognize = new RecognizeStrategyRanged();
        MoveBasic = new MoveStrategyFly();
        MoveChase = new MoveStrategyChase();
    }
}