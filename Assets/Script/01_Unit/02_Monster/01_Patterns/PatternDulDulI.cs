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

        switch (monster.GetStatus())
        {
            case MonsterStatus.Idle:
                Recognize?.PlayStrategy();
                MoveBasic?.PlayStrategy();
                break;
            case MonsterStatus.Chase:
                Recognize?.PlayStrategy();
                if (Attack?.PlayStrategy() == false)
                {
                    MoveChase?.PlayStrategy();
                }
                break;
            case MonsterStatus.AttackEnd:
                Groggy?.PlayStrategy();
                break;
        }
    }

}