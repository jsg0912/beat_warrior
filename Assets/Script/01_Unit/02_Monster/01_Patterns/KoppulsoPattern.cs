public class KoppulsoPattern : Pattern
{
    public KoppulsoPattern()
    {
        Recognize = new RecognizeStrategyMelee();
        MoveBasic = new MoveStrategyNormal();
        MoveChase = new MoveStrategyChase();
        Attack = new AttackStrategyRushKoppulso();
    }
}