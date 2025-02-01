public class PatternDulDulI : Pattern
{
    public PatternDulDulI()
    {
        Recognize = new RecognizeStrategyMelee();
        Attack = new AttackStrategyRolling();
    }
}