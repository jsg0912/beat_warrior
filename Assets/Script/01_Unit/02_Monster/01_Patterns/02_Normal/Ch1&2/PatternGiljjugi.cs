public class PatternGiljjugi : Pattern
{
    public PatternGiljjugi()
    {
        Recognize = new RecognizeStrategyMelee();
        MoveBasic = new MoveStrategyNormal();
        MoveChase = new MoveStrategyChase();
        Attack = new AttackStrategyMelee();
    }
}