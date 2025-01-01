public class IbkkugiPattern : Pattern
{
    public IbkkugiPattern()
    {
        Recognize = new RecognizeStrategyMelee();
        MoveNormal = new MoveStrategyNormal();
        MoveChase = new MoveStrategyChase();
        Attack = new AttackStrategyThrowIbkkugi();
    }
}