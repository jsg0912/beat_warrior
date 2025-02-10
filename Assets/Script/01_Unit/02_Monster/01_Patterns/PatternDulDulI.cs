public class PatternDulDulI : Pattern
{
    public PatternDulDulI()
    {
        Recognize = new RecognizeStrategyMelee();
        MoveBasic = new MoveStrategyNormal();
        MoveChase = new MoveStrategyChase();
        Attack = new AttackStrategyRolling(MonsterConstant.DulduliJumpPower, MonsterConstant.DulduliJumpNumber, MonsterConstant.DulduliJumpDuration);
        Groggy = new GroggyStrategyZeroDef(MonsterConstant.DulduliGroggyDuration);
    }

    public override void PlayPattern()
    {
        Attack?.UpdateCoolTime();

        if (monster.GetStatus() == MonsterStatus.Groggy)
        {
            Groggy?.PlayStrategy();
            return;
        }

        if (!monster.isChasing)
        {
            Recognize?.PlayStrategy();
            MoveBasic?.PlayStrategy();
            return;
        }

        Recognize?.PlayStrategy();
        if (Attack?.PlayStrategy() == false)
        {
            MoveChase?.PlayStrategy();
        }
    }
}