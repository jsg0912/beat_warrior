public class IppaliPattern : Pattern
{
    public IppaliPattern()
    {
        Recognize = new RecognizeStrategyMelee();
        MoveNormal = new MoveStrategyNormal();
        MoveChase = new MoveStrategyChase();
        Attack = new AttackStrategyMelee();
    }
}