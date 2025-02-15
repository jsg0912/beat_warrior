public class PatternDulDulI : Pattern
{
    public PatternDulDulI()
    {
        Recognize = new RecognizeStrategyMelee();
        MoveBasic = new MoveStrategyNormal();
        MoveChase = new MoveStrategyChase();
        Attack = new AttackStrategyRolling(MonsterConstant.DulduliJumpPower, MonsterConstant.DulduliJumpDuration, MonsterConstant.DulduliSplashRange);
        Groggy = new GroggyStrategyZeroDef(MonsterConstant.DulduliGroggyDuration);
    }
}