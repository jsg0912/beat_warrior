public class PatternDulDulI : Pattern
{
    public PatternDulDulI()
    {
        Recognize = new RecognizeStrategyMelee();
        MoveBasic = new MoveStrategyNormal();
        MoveChase = new MoveStrategyChase();
        Attack = new AttackStrategyRollingDulDulI();
    }
}