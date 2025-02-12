public class PatternIppali : Pattern
{
    public PatternIppali()
    {
        Recognize = new RecognizeStrategyMelee();
        MoveBasic = new MoveStrategyNormal();
        MoveChase = new MoveStrategyChase();
        Attack = new AttackStrategyMelee();
    }
}