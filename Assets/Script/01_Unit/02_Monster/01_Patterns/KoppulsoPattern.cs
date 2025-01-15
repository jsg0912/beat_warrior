public class KoppulsoPattern : Pattern
{
    public KoppulsoPattern()
    {
        Recognize = new RecognizeStrategyMelee();
        MoveNormal = new MoveStrategyNormal();
        MoveChase = new MoveStrategyChase();
        Attack = new AttackStrategyRushKoppulso();
    }
}