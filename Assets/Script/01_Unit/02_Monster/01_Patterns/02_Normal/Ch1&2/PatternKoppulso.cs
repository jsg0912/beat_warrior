public class PatternKoppulso : Pattern
{
    public PatternKoppulso()
    {
        Recognize = new RecognizeStrategyMelee();
        MoveBasic = new MoveStrategyNormal();
        MoveChase = new MoveStrategyChase();
        Attack = new AttackStrategyRush(MonsterConstant.KoppulsoRushSpeed, MonsterConstant.KoppulsoRushDuration);
    }
}